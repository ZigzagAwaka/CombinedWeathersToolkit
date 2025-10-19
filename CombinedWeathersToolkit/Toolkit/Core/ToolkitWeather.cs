using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WeatherRegistry;
using WeatherTweaks.Definitions;

namespace CombinedWeathersToolkit.Toolkit.Core
{
    public class ToolkitWeather
    {
        public CustomWeatherType? Type;
        public string? Name;
        public Color? NameColor;
        public int? Weight;
        public float? ScrapAmountMultiplier;
        public float? ScrapValueMultiplier;
        public bool? Filtering;
        public string? LevelFilter;
        public string? LevelWeights;
        public string? WeatherToWeatherWeights;
        public float? WeightModifier;
        public List<float> ProgressingTimes = new List<float>();
        public List<float> ProgressingChances = new List<float>();
        public List<WeatherResolvable> Weathers = new List<WeatherResolvable>();

        public void SetTypeFromString(string typeString)
        {
            Type = typeString.ToLower() switch
            {
                "combined" => CustomWeatherType.Combined,
                "combo" => CustomWeatherType.Combined,
                "progressing" => CustomWeatherType.Progressing,
                "progressive" => CustomWeatherType.Progressing,
                _ => CustomWeatherType.Normal
            };
        }

        public void SetColorFromString(string colorString)
        {
            if (colorString[0] == '#')
            {
                if (ColorUtility.TryParseHtmlString(colorString, out var customColor))
                    NameColor = customColor;
                else
                    NameColor = null;
                return;
            }
            NameColor = colorString.ToLower() switch
            {
                "red" => Color.red,
                "green" => Color.green,
                "blue" => Color.blue,
                "yellow" => Color.yellow,
                "cyan" => Color.cyan,
                "magenta" => Color.magenta,
                "black" => Color.black,
                "white" => Color.white,
                "gray" => Color.gray,
                "grey" => Color.grey,
                "clear" => Color.clear,
                _ => null
            };
        }

        public void AddProgressingValues(float[] values, bool areTimesValues)
        {
            foreach (var value in values)
            {
                if (value < 0f)
                    continue;
                if (areTimesValues)
                    ProgressingTimes.Add(value);
                else
                    ProgressingChances.Add(value);
            }
        }

        public void AddWeather(string weatherName)
        {
            if (string.IsNullOrEmpty(weatherName))
                return;
            WeatherResolvable weatherResolvable = string.Concat(weatherName.Where(c => !char.IsWhiteSpace(c))).ToLower() switch
            {
                "none" => new WeatherTypeResolvable(LevelWeatherType.None),
                "dustclouds" => new WeatherTypeResolvable(LevelWeatherType.DustClouds),
                "rainy" => new WeatherTypeResolvable(LevelWeatherType.Rainy),
                "stormy" => new WeatherTypeResolvable(LevelWeatherType.Stormy),
                "foggy" => new WeatherTypeResolvable(LevelWeatherType.Foggy),
                "flooded" => new WeatherTypeResolvable(LevelWeatherType.Flooded),
                "eclipsed" => new WeatherTypeResolvable(LevelWeatherType.Eclipsed),
                _ => new WeatherNameResolvable(weatherName)
            };
            Weathers.Add(weatherResolvable);
        }

        public void AddWeathers(string[] weatherNames)
        {
            foreach (string weatherName in weatherNames)
            {
                AddWeather(weatherName);
            }
        }

        public List<ProgressingWeatherEntry> GetProgressingWeatherEntries()
        {
            var result = new List<ProgressingWeatherEntry>();
            for (int i = 1; i < Weathers.Count; i++)
            {
                result.Add(new ProgressingWeatherEntry() { DayTime = ProgressingTimes[i - 1], Chance = ProgressingChances[i - 1], Weather = Weathers[i] });
            }
            return result;
        }

        public bool AreWeathersInstalled()
        {
            foreach (var weather in Weathers)
            {
                if (weather.WeatherName != "" && !Plugin.installedWeathers.Any(w => w == weather.WeatherName))
                    return false;
            }
            return true;
        }

        public bool IsValid()
        {
            if (Type == null || Type == CustomWeatherType.Normal)  // set Type to Combined if not specified
                Type = CustomWeatherType.Combined;

            if (string.IsNullOrEmpty(Name))  // make sure Name is set
                return false;

            if (!AreWeathersInstalled())  // make sure all weathers are installed
                return false;

            if (Type == CustomWeatherType.Combined)  // combined weathers needs to have at least 1 weather
                return Weathers.Count >= 1;
            else if (Type == CustomWeatherType.Progressing)  // progressive weathers needs...
            {
                if (Weathers.Count <= 1)  // to have at least 2 weathers
                    return false;
                if (Weathers.Count - 1 == ProgressingTimes.Count && Weathers.Count - 1 == ProgressingChances.Count)  // and to have progressive values set
                    return true;
                if (ProgressingTimes.Count != 0 && ProgressingChances.Count != 0)  // or to have one or both of the values empty if not set
                    return false;
                if ((ProgressingTimes.Count == 0 && ProgressingChances.Count == 0) ||
                    (ProgressingTimes.Count == 0 && ProgressingChances.Count == Weathers.Count - 1) ||
                    (ProgressingChances.Count == 0 && ProgressingTimes.Count == Weathers.Count - 1))  // and that one is empty and the other is set, or both empty
                {
                    if (ProgressingTimes.Count == 0)  // fill with default times
                    {
                        var steps = 1f / Weathers.Count;
                        for (int i = 0; i < Weathers.Count - 1; i++)
                        {
                            ProgressingTimes.Add(steps * (i + 1));
                        }
                    }
                    if (ProgressingChances.Count == 0)  // fill with default chances
                    {
                        ProgressingChances.AddRange(Weathers.Select(x => 1f));
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool Register()
        {
            if (!IsValid())
                return false;
            switch (Type)
            {
                case CustomWeatherType.Combined:
                    new ToolkitCombinedWeatherType(this);
                    return true;
                case CustomWeatherType.Progressing:
                    new ToolkitProgressingWeatherType(this);
                    return true;
                default:
                    break;
            }
            return false;
        }
    }
}
