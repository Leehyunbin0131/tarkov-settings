using System;
using System.Management;

namespace tarkov_settings.GPU
{
    public enum GPUVendor
    {
        NVIDIA = 1,
        AMD = 2,
        Intel = 3,
        Unknown = 4
    }

    class GPUDevice
    {
        private static readonly Lazy<IGPU> instance =
            new Lazy<IGPU>(() =>
            {
                var vendor = GPUVendor.Unknown;
                var selectedDeviceName = "Unknown GPU";
                try
                {
                    using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            var deviceName = (obj["Name"] ?? "Unknown GPU").ToString();
                            var normalizedName = deviceName.ToUpperInvariant();

                            if (normalizedName.Contains("NVIDIA") || normalizedName.Contains("GEFORCE"))
                            {
                                vendor = GPUVendor.NVIDIA;
                                selectedDeviceName = deviceName;
                                break;
                            }
                            else if (normalizedName.Contains("AMD") || normalizedName.Contains("RADEON"))
                            {
                                vendor = GPUVendor.AMD;
                                selectedDeviceName = deviceName;
                            }
                            else if (normalizedName.Contains("INTEL") && vendor == GPUVendor.Unknown)
                            {
                                vendor = GPUVendor.Intel;
                                selectedDeviceName = deviceName;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    tarkov_settings.AppLogger.Error("GPU detection failed", ex);
                }

                switch (vendor)
                {
                    case GPUVendor.NVIDIA:
                        return new NVIDIA(vendor, selectedDeviceName);
                    case GPUVendor.AMD:
                        return new AMD(vendor, selectedDeviceName);
                    case GPUVendor.Intel:
                        return new UnsupportedGPU(vendor, selectedDeviceName);
                    default:
                        return new UnsupportedGPU(GPUVendor.Unknown, selectedDeviceName);
                }
            });
        public static IGPU Instance
        {
            get => instance.Value;
        }
    }
}
