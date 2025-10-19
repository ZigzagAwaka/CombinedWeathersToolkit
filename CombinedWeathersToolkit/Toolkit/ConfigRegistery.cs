using CombinedWeathersToolkit.Toolkit.Core;
using System.Linq;

namespace CombinedWeathersToolkit.Toolkit
{
    internal class ConfigRegistery
    {
        internal static void LoadAllConfigValues()
        {
            if (Plugin.config.WeatherConfigCreatorList.Count == 0)
            {
                return;
            }

            int nbWeathers = 0;
            Plugin.DebugLog($"[ConfigRegistery] Found values in config file, now loading Weather Config creator");

            foreach (var weatherString in Plugin.config.WeatherConfigCreatorList)
            {
                var weather = new ToolkitWeather();
                if (string.IsNullOrWhiteSpace(weatherString))
                    return;
                int valuePos = 0;
                foreach (string value in weatherString.Split(':').Select(s => s.Trim()))
                {
                    //Plugin.logger.LogError(value);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        Plugin.logger.LogError("[ConfigRegistery] One of the field in Weather Config creator is empty or null, this could result in an unwanted behavior");
                        continue;
                    }
                    if (valuePos == 0)
                    {
                        weather.Name = value;
                    }
                    else if (value.ToLower().StartsWith("color("))
                    {
                        weather.SetColorFromString(value[6..^1]);  // remove "color(" at start and ")" at end
                    }
                    else if (value.ToLower().StartsWith("type("))
                    {
                        weather.SetTypeFromString(value[5..^1]);  // remove "type(" at start and ")" at end
                    }
                    else
                    {
                        weather.AddWeather(value);
                    }
                    valuePos++;
                }

                if (weather.Register())
                {
                    nbWeathers++;
                    Plugin.DebugLog($"[ConfigRegistery] Registered weather: {weather.Name}");
                }
                else
                {
                    Plugin.DebugLog($"[ConfigRegistery] The '{weatherString}' value is not valid and could not register a weather ; this can happen if the name of the weather was not set or if one of the added weathers is a modded weather and the mod it's from it not installed");
                }
            }
            Plugin.logger.LogInfo($"[ConfigRegistery] Loaded {nbWeathers} custom weathers from Weather Config creator");
        }
    }
}
