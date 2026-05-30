namespace tarkov_settings.GPU
{
    internal interface IGPU
    {
        GPUVendor Vendor { get; }
        string DeviceName { get; }
        bool SupportsSaturation { get; }
        int Saturation { get; set; }
        int MaxSaturation { get; }
        int MinSaturation { get; }
        int InitSaturation { get; }
        void ResetSaturation();
        void Load(string display);
        void Close();
    }
}
