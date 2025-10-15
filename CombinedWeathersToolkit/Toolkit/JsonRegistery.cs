using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

namespace CombinedWeathersToolkit.Toolkit
{
    internal class JsonRegistery
    {
        internal static void LoadAllJsonFiles()
        {
            var jsonFiles = Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ".."),
                $"*.cwt.json", SearchOption.AllDirectories);

            foreach (var filePath in jsonFiles)
            {
                var fileName = Path.GetFileName(filePath);
                if (!Plugin.config.RegisterPredefinedCombinedWeathers.Value && fileName.Equals(Plugin.PredefinedFileName))
                    continue;
                Plugin.VerboseLog($"[JsonRegistery] Found json file, now loading: {fileName}");
                RegisterWeathersFromJson(filePath);
                Plugin.logger.LogInfo($"[JsonRegistery] Loaded json file: {fileName}");
            }
        }

        private static void RegisterWeathersFromJson(string filePath)
        {
            var jsonObject = JObject.Parse(File.ReadAllText(filePath));
            foreach (var (key, value) in jsonObject)
            {
                Plugin.logger.LogError(key + " " + value);
            }
        }
    }
}
