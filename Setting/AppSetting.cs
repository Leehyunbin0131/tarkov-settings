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
        public const int PresetCount = 4;
        public static readonly string[] PresetNames =
        {
            "Preset 1",
            "Preset 2",
            "Preset 3",
            "Preset 4"
        };

        public int schemaVersion = 0;

        public string activeProfile = "Preset 1";
        public Dictionary<string, ColorProfile> profiles = new Dictionary<string, ColorProfile>
        {
            {
                "Preset 1",
                new ColorProfile
                {
                    name = "Preset 1",
                    brightness = 0.5,
                    contrast = 0.5,
                    gamma = 1.0,
                    saturation = 0,
                    display = @"\\.\DISPLAY1"
                }
            },
            {
                "Preset 2",
                new ColorProfile
                {
                    name = "Preset 2",
                    brightness = 0.5,
                    contrast = 0.5,
                    gamma = 1.0,
                    saturation = 0,
                    display = @"\\.\DISPLAY1"
                }
            },
            {
                "Preset 3",
                new ColorProfile
                {
                    name = "Preset 3",
                    brightness = 0.5,
                    contrast = 0.5,
                    gamma = 1.0,
                    saturation = 0,
                    display = @"\\.\DISPLAY1"
                }
            },
            {
                "Preset 4",
                new ColorProfile
                {
                    name = "Preset 4",
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
        public string language = "en";

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
            var shouldMigrateLegacyValues = schemaVersion < 3;
            var previousActiveProfile = activeProfile;
            schemaVersion = 4;

            if (pTargets == null || pTargets.Count == 0)
                pTargets = new HashSet<string> { "EscapeFromTarkov" };

            if (!IsSupportedLanguage(language))
                language = "en";

            if (profiles == null || profiles.Count == 0)
                profiles = new Dictionary<string, ColorProfile>();

            var legacyProfile = shouldMigrateLegacyValues
                ? CreateLegacyProfile()
                : GetLegacyProfile(previousActiveProfile);

            for (var i = 0; i < PresetNames.Length; i++)
            {
                var presetName = PresetNames[i];
                if (!profiles.ContainsKey(presetName))
                    profiles[presetName] = CreateDefaultProfile(presetName);
            }

            if (string.IsNullOrWhiteSpace(activeProfile) || Array.IndexOf(PresetNames, activeProfile) < 0)
                activeProfile = PresetNames[0];

            if (shouldMigrateLegacyValues)
            {
                profiles[PresetNames[0]].brightness = legacyProfile.brightness;
                profiles[PresetNames[0]].contrast = legacyProfile.contrast;
                profiles[PresetNames[0]].gamma = legacyProfile.gamma;
                profiles[PresetNames[0]].saturation = legacyProfile.saturation;
                profiles[PresetNames[0]].display = legacyProfile.display;
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

        private ColorProfile GetLegacyProfile(string profileName)
        {
            if (!string.IsNullOrWhiteSpace(profileName) && profiles.ContainsKey(profileName))
                return profiles[profileName];

            return CreateLegacyProfile();
        }

        private ColorProfile CreateLegacyProfile()
        {
            return new ColorProfile
            {
                name = PresetNames[0],
                brightness = brightness,
                contrast = contrast,
                gamma = gamma,
                saturation = saturation,
                display = display
            };
        }

        private ColorProfile CreateDefaultProfile(string presetName)
        {
            return new ColorProfile
            {
                name = presetName,
                brightness = 0.5,
                contrast = 0.5,
                gamma = 1.0,
                saturation = 0,
                display = display
            };
        }

        private bool IsSupportedLanguage(string value)
        {
            return value == "en" || value == "ko" || value == "ja" || value == "zh";
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
