using System;
using System.Windows.Forms;

using tarkov_settings.GPU;

namespace tarkov_settings
{
    static class Program
    {
        private static MainForm mForm;

        [STAThread]
        static void Main(string[] args)
        {
            IGPU gpu = null;
            try
            {
                gpu = GPUDevice.Instance;
                if (HasArg(args, "--reset"))
                {
                    if (Display.displays.Count > 0)
                        Display.Primary = Display.displays[0];

                    ColorController.Instance.ResetToDefaultRamp();
                    gpu.Close();
                    return;
                }

                if(gpu.Vendor == GPUVendor.AMD)
                {
                    /* AMD Saturation (equals to Digital Vibrance of Nvidia) is not supported yet. */
                    System.Windows.Forms.MessageBox.Show(
                            "AMD Device Detected - Saturation is not supported yet.",
                            "Warning",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Warning
                        );
                }
                else if (gpu.Vendor == GPUVendor.Intel || gpu.Vendor == GPUVendor.Unknown)
                {
                    System.Windows.Forms.MessageBox.Show(
                            $"{gpu.Vendor} GPU Detected - Saturation is disabled, but brightness/contrast/gamma can still be used.",
                            "Limited GPU Support",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Warning
                        );
                }
            } catch (Exception ex)
            {
                AppLogger.Error("GPU initialization failed", ex);
                System.Windows.Forms.MessageBox.Show(
                        "GPU initialization failed. Brightness/contrast/gamma may still work, but saturation will be disabled.",
                        "GPU Warning",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Warning
                    );
            }

            // Open Main Form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mForm = new MainForm(GetArgValue(args, "--profile"), HasArg(args, "--minimized"));
            Application.Run(mForm);

            // Unload NvAPI dll after Application.Exit()
            if(gpu != null)
                gpu.Close();
        }

        private static bool HasArg(string[] args, string argName)
        {
            foreach (var arg in args)
            {
                if (string.Equals(arg, argName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private static string GetArgValue(string[] args, string argName)
        {
            for (var i = 0; i < args.Length - 1; i++)
            {
                if (string.Equals(args[i], argName, StringComparison.OrdinalIgnoreCase))
                    return args[i + 1];
            }

            return null;
        }
    }
}
