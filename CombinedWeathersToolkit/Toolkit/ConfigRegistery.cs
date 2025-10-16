using CombinedWeathersToolkit.Toolkit.Core;
using System.Linq;
using WeatherTweaks.Definitions;

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
                if (weather.Type == null || weather.Type == CustomWeatherType.Normal)
                {
                    weather.Type = CustomWeatherType.Combined;  // default to combined if not specified
                }

                if (weather.Register())
                {
                    nbWeathers++;
                    Plugin.DebugLog($"[ConfigRegistery] Registered weather: {weather.Name}");
                }
                else
                {
                    Plugin.logger.LogError($"[ConfigRegistery] The '{weatherString}' value is not valid and could not register a weather");
                }
            }
            Plugin.logger.LogInfo($"[ConfigRegistery] Loaded {nbWeathers} combined weathers from Weather Config creator");
        }
    }
}
