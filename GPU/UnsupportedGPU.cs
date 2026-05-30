namespace tarkov_settings.GPU
{
    class UnsupportedGPU : IGPU
    {
        private readonly GPUVendor vendor;
        private readonly string deviceName;

        public UnsupportedGPU(GPUVendor vendor, string deviceName)
        {
            this.vendor = vendor;
            this.deviceName = deviceName;
        }

        public GPUVendor Vendor => vendor;
        public string DeviceName => deviceName;
        public bool SupportsSaturation => false;
        public int Saturation { get; set; }
        public int MaxSaturation => 0;
        public int MinSaturation => 0;
        public int InitSaturation => 0;

        public void ResetSaturation()
        {
            Saturation = InitSaturation;
        }

        public void Load(string display)
        {
            tarkov_settings.AppLogger.Info($"Unsupported GPU ({Vendor}: {DeviceName}) loaded for {display}. Saturation is disabled.");
        }

        public void Close()
        {
        }
    }
}
