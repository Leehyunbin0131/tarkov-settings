using System;

namespace tarkov_settings.GPU
{
    class AMD : IGPU
    {
        private GPUVendor _vendor;
        private string _deviceName;
        private int currentSaturation;

        public GPUVendor Vendor
        {
            get => this._vendor;
        }

        public string DeviceName => _deviceName;

        public bool SupportsSaturation => false;

        public int Saturation
        {
            get => currentSaturation;
            set => currentSaturation = value;
        }

        public int MaxSaturation => 0;

        public int MinSaturation => 0;

        public int InitSaturation => 0;

        public AMD(GPUVendor vendor, string deviceName)
        {
            this._vendor = vendor;
            this._deviceName = deviceName;
        }

        public void ResetSaturation()
        {
            currentSaturation = InitSaturation;
        }

        public void Load(string display)
        {
            tarkov_settings.AppLogger.Info($"AMD GPU detected. Saturation is disabled for {display}.");
        }

        public void Close()
        {
        }
    }
}
