using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using tarkov_settings.GPU;

namespace tarkov_settings
{
    class ColorController
    {
        private IGPU gpu = GPUDevice.Instance;
        private readonly object rampLock = new object();

        // Gamma Ramps
        private RAMP currentRamps;
        private RAMP originalRamps;
        private bool initialized;

        /**
         * _canceller : Token Source to abort Async-Task (Gamma Value Change)
         * WHY : *I don't know why* set gamma ramp keeps revert soon after modified
         */
        private CancellationTokenSource _canceller;

        #region Singleton Pattern implement
        private static readonly Lazy<ColorController> instance =
            new Lazy<ColorController>(() => new ColorController());

        public static ColorController Instance
        {
            get
            {
                return instance.Value;
            }
        }
        #endregion

        #region Win32 API Calls
        [DllImport("gdi32")]
        private static extern bool GetDeviceGammaRamp(IntPtr hDc, ref RAMP lpRamp);

        [DllImport("gdi32")]
        private static extern bool SetDeviceGammaRamp(IntPtr hDc, ref RAMP lpRamp);
        #endregion

        public int DVL
        {
            get => gpu.Saturation;
            set
            {
                if (!gpu.SupportsSaturation)
                    return;

                try
                {
                    gpu.Saturation = value;
                }
                catch (Exception ex)
                {
                    AppLogger.Error("Failed to set saturation", ex);
                }
            }
        }

        private ColorController()
        {

        }

        public void Init()
        {
            // Backup Gamma Ramp
            lock (rampLock)
            {
                var hdc = IntPtr.Zero;
                try
                {
                    hdc = Display.CreateDC(null, Display.Primary, null, IntPtr.Zero);
                    currentRamps = new RAMP();
                    originalRamps = CreateDefaultRamp();

                    if (!IntPtr.Zero.Equals(hdc))
                        GetDeviceGammaRamp(hdc, ref originalRamps);

                    initialized = true;
                    AppLogger.Info($"Color controller initialized for {Display.Primary}.");
                }
                catch (Exception ex)
                {
                    originalRamps = CreateDefaultRamp();
                    initialized = true;
                    AppLogger.Error("Failed to back up gamma ramp. Default ramp will be used for reset.", ex);
                }
                finally
                {
                    if (!IntPtr.Zero.Equals(hdc))
                        Display.DeleteDC(hdc);
                }
            }
        }

        public void ChangeColorRamp(double brightness = 0.5, double contrast = 0.5, double gamma = 1.0, bool reset = true)
        {
            if (!initialized)
                Init();

            lock (rampLock)
            {
                StopRampLoopLocked();

                if (reset)
                {
                    ApplyRamp(originalRamps);
                    return;
                }

                ushort[] iArrayValue = CalculateLUT(brightness, contrast, gamma);
                currentRamps = new RAMP
                {
                    Red = iArrayValue,
                    Blue = iArrayValue,
                    Green = iArrayValue
                };

                _canceller = new CancellationTokenSource();
                var token = _canceller.Token;
                var ramps = currentRamps;

                Task.Run(() => ApplyRampLoop(ramps, token), token);
            }
        }

        /*
         * Code from
         * https://github.com/falahati/NvAPIWrapper/issues/20#issuecomment-634551206
         */
        private static ushort[] CalculateLUT(double brightness = 0.5, double contrast = 0.5, double gamma = 2.8)
        {
            const int dataPoints = 256;

            // Limit gamma in range [0.4-2.8]
            gamma = Math.Min(Math.Max(gamma, 0.4), 2.8);
            // Normalize contrast in range [-1,1]
            contrast = (Math.Min(Math.Max(contrast, 0), 1) - 0.5) * 2;
            // Normalize brightness in range [-1,1]
            brightness = (Math.Min(Math.Max(brightness, 0), 1) - 0.5) * 2;
            // Calculate curve offset resulted from contrast
            var offset = contrast > 0 ? contrast * -25.4 : contrast * -32;
            // Calculate the total range of curve
            var range = (dataPoints - 1) + offset * 2;
            // Add brightness to the curve offset
            offset += brightness * (range / 5);
            // Fill the gamma curve
            var result = new ushort[dataPoints];
            for (var i = 0; i < result.Length; i++)
            {
                var factor = (i + offset) / range;
                factor = Math.Pow(factor, 1 / gamma);
                factor = Math.Min(Math.Max(factor, 0), 1);
                result[i] = (ushort)Math.Round(factor * ushort.MaxValue);
            }
            return result;
        }

        public void ResetDVL()
        {
            try
            {
                gpu.ResetSaturation();
                Console.WriteLine("[DVL] Reset to : {0}", gpu.InitSaturation);
                AppLogger.Info($"DVL reset to {gpu.InitSaturation}.");
            }
            catch (Exception ex)
            {
                AppLogger.Error("Failed to reset DVL", ex);
            }
        }

        public void ResetAll()
        {
            lock (rampLock)
            {
                StopRampLoopLocked();
                ApplyRamp(originalRamps);
            }

            ResetDVL();
        }

        public void ResetToDefaultRamp()
        {
            lock (rampLock)
            {
                StopRampLoopLocked();
                ApplyRamp(CreateDefaultRamp());
            }
        }

        internal void Close()
        {
            ResetAll();
        }

        private void ApplyRampLoop(RAMP ramps, CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    ApplyRamp(ramps);
                    if (token.WaitHandle.WaitOne(250))
                        break;
                }
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception ex)
            {
                AppLogger.Error("Gamma ramp loop failed", ex);
            }
        }

        private bool ApplyRamp(RAMP ramps)
        {
            var hdc = IntPtr.Zero;
            try
            {
                hdc = Display.CreateDC(null, Display.Primary, null, IntPtr.Zero);
                if (IntPtr.Zero.Equals(hdc))
                    return false;

                return SetDeviceGammaRamp(hdc, ref ramps);
            }
            catch (Exception ex)
            {
                AppLogger.Error("Failed to apply gamma ramp", ex);
                return false;
            }
            finally
            {
                if (!IntPtr.Zero.Equals(hdc))
                    Display.DeleteDC(hdc);
            }
        }

        private void StopRampLoopLocked()
        {
            try
            {
                if (_canceller == null)
                    return;

                _canceller.Cancel();
                _canceller.Dispose();
                _canceller = null;
            }
            catch (ObjectDisposedException)
            {
                _canceller = null;
            }
        }

        private static RAMP CreateDefaultRamp()
        {
            var values = new ushort[256];
            for (var i = 0; i < values.Length; i++)
                values[i] = (ushort)(i * 257);

            return new RAMP
            {
                Red = values,
                Green = values,
                Blue = values
            };
        }

    }
}
