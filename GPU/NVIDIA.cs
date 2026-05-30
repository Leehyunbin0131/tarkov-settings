using System;
using System.Windows.Forms;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.Display.Structures;

namespace tarkov_settings.GPU
{
    class NVIDIA : IGPU
    {
        private GPUVendor _vendor;
        private string _deviceName;
        private DisplayHandle displayHandle;
        private bool displayLoaded;

        private int _maxSaturation;
        private int _minSaturation;
        private int _initSaturation;
        private int currentSaturation;

        public GPUVendor Vendor
        {
            get => this._vendor;
        }

        public string DeviceName => _deviceName;

        public bool SupportsSaturation => true;

        public int MaxSaturation
        {
            get => _maxSaturation;
        }

        public int MinSaturation
        {
            get => _minSaturation;
        }

        public int InitSaturation
        {
            get => _initSaturation;
        }

        public int Saturation
        {
            get => currentSaturation;
            set
            {
                if (value > this.MaxSaturation)
                    value = this.MaxSaturation;
                if (value < this.MinSaturation)
                    value = this.MinSaturation;

                DisplayApi.SetDVCLevel(displayHandle, value);
                this.currentSaturation = value;
            }
        }

        public NVIDIA(GPUVendor vendor, string deviceName)
        {
            try
            { 
                NvAPIWrapper.NVIDIA.Initialize();
            }
            catch (NvAPIWrapper.Native.Exceptions.NVIDIAApiException)
            {
                MessageBox.Show("NvAPI Intialize Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this._vendor = vendor;
            this._deviceName = deviceName;
        }

        public void ResetSaturation()
        {
            if (displayLoaded)
                this.Saturation = this.InitSaturation;
        }

        public void Load(string display) {
            try
            {
                displayHandle = DisplayApi.GetAssociatedNvidiaDisplayHandle(display);
                PrivateDisplayDVCInfo dvcInfo = DisplayApi.GetDVCInfo(displayHandle);
                this._maxSaturation = dvcInfo.MaximumLevel;
                this._minSaturation = dvcInfo.MinimumLevel;
                this._initSaturation = this.currentSaturation = dvcInfo.CurrentLevel;
                displayLoaded = true;
            }
            catch (Exception ex)
            {
                displayLoaded = false;
                tarkov_settings.AppLogger.Error($"Failed to load NVIDIA DVC info for {display}", ex);
                throw;
            }
        }

        public void Close() {
            try
            {
                NvAPIWrapper.NVIDIA.Unload();
            }
            catch (NvAPIWrapper.Native.Exceptions.NVIDIAApiException)
            {
                MessageBox.Show("NvAPI Unload Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
