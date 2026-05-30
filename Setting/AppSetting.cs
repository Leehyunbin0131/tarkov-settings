using System;
using System.Collections.Generic;

namespace tarkov_settings.Setting
{
    class ColorProfile
    {
        public string name = "Tarkov";
        public double brightness = 0.5;
        public double contrast = 0.5;
        public double gamma = 1.0;
        public int saturation = 0;
        public string display = @"\\.\DISPLAY1";
    }

    class AppSetting : Settings<AppSetting>
    {
        public int schemaVersion = 0;

        public string activeProfile = "Tarkov";
        public Dictionary<string, ColorProfile> profiles = new Dictionary<string, ColorProfile>
        {
            {
                "Tarkov",
                new ColorProfile
                {
                    name = "Tarkov",
                    brightness = 0.5,
                    contrast = 0.5,
                    gamma = 1.0,
                    saturation = 0,
                    display = @"\\.\DISPLAY1"
                }
            },
            {
                "Default",
                new ColorProfile
                {
                    name = "Default",
                    brightness = 0.5,
                    contrast = 0.5,
                    gamma = 1.0,
                    saturation = 0,
                    display = @"\\.\DISPLAY1"
                }
            }
        };

        // Legacy fields are kept so existing settings.json files migrate cleanly.
        public double brightness = 0.5;
        public double contrast = 0.5;
        public double gamma = 1.0;
        public int saturation = 0;
        public HashSet<string> pTargets = new HashSet<string>{
            "EscapeFromTarkov"
        };
        public string display = @"\\.\DISPLAY1";
        public bool minimizeOnStart = false;
        public bool startWithWindows = false;
        public bool enableHotkeys = true;

        public static new AppSetting Load(string fileName = Settings<AppSetting>.DEFAULT_FILENAME)
        {
            var settings = Settings<AppSetting>.Load(fileName);
            settings.Normalize();
            return settings;
        }

        public ColorProfile GetActiveProfile()
        {
            Normalize();
            return profiles[activeProfile];
        }

        public void SetActiveProfile(string profileName)
        {
            if (!string.IsNullOrWhiteSpace(profileName) && profiles.ContainsKey(profileName))
            {
                activeProfile = profileName;
                ApplyActiveProfileToLegacyFields();
            }
        }

        public void UpdateActiveProfile(double brightness, double contrast, double gamma, int saturation, string display)
        {
            Normalize();
            var profile = profiles[activeProfile];
            profile.brightness = brightness;
            profile.contrast = contrast;
            profile.gamma = gamma;
            profile.saturation = saturation;
            profile.display = display;
            ApplyActiveProfileToLegacyFields();
        }

        public void Normalize()
        {
            var shouldMigrateLegacyValues = schemaVersion < 2;
            schemaVersion = 2;

            if (pTargets == null || pTargets.Count == 0)
                pTargets = new HashSet<string> { "EscapeFromTarkov" };

            if (profiles == null || profiles.Count == 0)
                profiles = new Dictionary<string, ColorProfile>();

            if (string.IsNullOrWhiteSpace(activeProfile))
                activeProfile = "Tarkov";

            if (!profiles.ContainsKey(activeProfile))
            {
                profiles[activeProfile] = new ColorProfile
                {
                    name = activeProfile,
                    brightness = brightness,
                    contrast = contrast,
                    gamma = gamma,
                    saturation = saturation,
                    display = display
                };
            }

            if (!profiles.ContainsKey("Default"))
            {
                profiles["Default"] = new ColorProfile
                {
                    name = "Default",
                    brightness = 0.5,
                    contrast = 0.5,
                    gamma = 1.0,
                    saturation = 0,
                    display = display
                };
            }

            if (shouldMigrateLegacyValues)
            {
                profiles[activeProfile].brightness = brightness;
                profiles[activeProfile].contrast = contrast;
                profiles[activeProfile].gamma = gamma;
                profiles[activeProfile].saturation = saturation;
                profiles[activeProfile].display = display;
            }

            foreach (var pair in profiles)
            {
                if (string.IsNullOrWhiteSpace(pair.Value.name))
                    pair.Value.name = pair.Key;
                if (string.IsNullOrWhiteSpace(pair.Value.display))
                    pair.Value.display = display;
            }

            ApplyActiveProfileToLegacyFields();
        }

        private void ApplyActiveProfileToLegacyFields()
        {
            var profile = profiles[activeProfile];
            brightness = profile.brightness;
            contrast = profile.contrast;
            gamma = profile.gamma;
            saturation = profile.saturation;
            display = profile.display;
        }
    }
}
