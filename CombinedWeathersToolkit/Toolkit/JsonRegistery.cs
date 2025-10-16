using CombinedWeathersToolkit.Toolkit.Core;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Reflection;
using WeatherTweaks.Definitions;

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
                if (Path.GetDirectoryName(filePath).EndsWith("Examples"))
                    continue;
                var fileName = Path.GetFileName(filePath);
                Plugin.DebugLog($"[JsonRegistery] Found json file, now loading: {fileName}");
                var nbWeathers = RegisterWeathersFromJson(filePath);
                Plugin.logger.LogInfo($"[JsonRegistery] Loaded {nbWeathers} combined weathers from json file: {fileName}");
            }
        }


        private static int RegisterWeathersFromJson(string filePath)
        {
            var text = File.ReadAllText(filePath);
            if (text == null || text.Length <= 0)
            {
                Plugin.logger.LogError("[JsonRegistery] The file is empty or null");
                return 0;
            }

            var nbWeathers = 0;
            var jsonObject = JObject.Parse(text);

            foreach (var weatherProperty in jsonObject.Properties())
            {
                string weatherKey = weatherProperty.Name;
                JToken weatherValue = weatherProperty.Value;
                var weather = new ToolkitWeather();

                if (weatherValue.Type == JTokenType.Object)
                {
                    foreach (var property in ((JObject)weatherValue).Properties())
                    {
                        //Plugin.logger.LogError(property.Name + ": " + property.Value);
                        switch (property.Name.ToLower())
                        {
                            case "type":
                                weather.SetTypeFromString(ExtractJsonProperty<string>(property));
                                break;
                            case "name":
                                weather.Name = ExtractJsonProperty<string>(property);
                                break;
                            case "color":
                                weather.SetColorFromString(ExtractJsonProperty<string>(property));
                                break;
                            case "weight":
                                weather.Weight = ExtractJsonProperty<int>(property);
                                break;
                            case "scrap_amount":
                                weather.ScrapAmountMultiplier = ExtractJsonProperty<float>(property);
                                break;
                            case "scrap_value":
                                weather.ScrapValueMultiplier = ExtractJsonProperty<float>(property);
                                break;
                            case "filtering":
                                weather.Filtering = ExtractJsonProperty<bool>(property);
                                break;
                            case "level_filter":
                                weather.LevelFilter = ExtractJsonProperty<string>(property);
                                break;
                            case "level_weights":
                                weather.LevelWeights = ExtractJsonProperty<string>(property);
                                break;
                            case "weather_to_weather_weights":
                                weather.WeatherToWeatherWeights = ExtractJsonProperty<string>(property);
                                break;
                            case "progressing_time":
                                weather.ProgressingTime = ExtractJsonProperty<float>(property);
                                break;
                            case "weathers":
                                weather.AddWeathers(ExtractJsonProperty<string[]>(property));
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    Plugin.logger.LogError($"[JsonRegistery] The '{weatherKey}' property is not an Object, skipping...");
                }
                if (weather.Type == null || weather.Type == CustomWeatherType.Normal)
                {
                    weather.Type = CustomWeatherType.Combined;  // default to combined if not specified
                }

                if (weather.Register())
                {
                    nbWeathers++;
                    Plugin.DebugLog($"[JsonRegistery] Registered weather: {weather.Name}");
                }
                else
                {
                    Plugin.logger.LogError($"[JsonRegistery] The '{weatherKey}' property is not valid and could not register a weather");
                }
            }
            return nbWeathers;
        }


        private static T ExtractJsonProperty<T>(JProperty property)
        {
            if (typeof(T) == typeof(string))
            {
                return (T)(object)property.Value.ToString();
            }
            else if (typeof(T) == typeof(int))
            {
                if (int.TryParse(property.Value.ToString(), out int intValue))
                    return (T)(object)intValue;
            }
            else if (typeof(T) == typeof(float))
            {
                if (float.TryParse(property.Value.ToString(), out float floatValue))
                    return (T)(object)floatValue;
            }
            else if (typeof(T) == typeof(bool))
            {
                if (bool.TryParse(property.Value.ToString(), out bool boolValue))
                    return (T)(object)boolValue;
            }
            else if (typeof(T) == typeof(string[]))
            {
                if (property.Value.Type == JTokenType.Array)
                {
                    var jsonArray = property.Value as JArray;
                    return (T)(object)jsonArray.Select(x => x.ToString()).ToArray();
                }
            }
            return default;
        }
    }
}
