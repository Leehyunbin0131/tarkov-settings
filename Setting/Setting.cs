using System;
using System.IO;
using Newtonsoft.Json;

namespace tarkov_settings.Setting
{
    internal class Settings<T> where T : new()
    {
        public const string DEFAULT_FILENAME = "settings.json";

        public static string DefaultFilePath => ResolveFileName(DEFAULT_FILENAME);

        public void Save(string fileName = DEFAULT_FILENAME)
        {
            var path = ResolveFileName(fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public static T Load(string fileName = DEFAULT_FILENAME)
        {
            T t = new T();
            var path = ResolveFileName(fileName);
            var legacyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            try
            {
                if (File.Exists(path))
                    t = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                else if (File.Exists(legacyPath))
                    t = JsonConvert.DeserializeObject<T>(File.ReadAllText(legacyPath));

                if (t == null)
                    t = new T();
            }
            catch (Exception ex)
            {
                tarkov_settings.AppLogger.Error($"Failed to load settings from {path}", ex);
                t = new T();
            }

            return t;
        }

        private static string ResolveFileName(string fileName)
        {
            if (Path.IsPathRooted(fileName))
                return fileName;

            return Path.Combine(AppLogger.AppDataDirectory, fileName);
        }
    }
}
