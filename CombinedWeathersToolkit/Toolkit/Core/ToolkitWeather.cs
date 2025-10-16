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
        public float? ProgressingTime;
        public List<WeatherResolvable> Weathers = new List<WeatherResolvable>();

        public void SetTypeFromString(string typeString)
        {
            Type = typeString.ToLower() switch
            {
                "combined" => CustomWeatherType.Combined,
                "progressing" => CustomWeatherType.Progressing,
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

        public void AddWeathers(string[] weatherNames)
        {
            foreach (string weatherName in weatherNames)
            {
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
        }

        public bool IsValid()
        {
            return Type != null && Type != CustomWeatherType.Normal && !string.IsNullOrEmpty(Name) && Weathers.Count > 0;
        }

        public bool Register()
        {
            switch (Type)
            {
                case CustomWeatherType.Combined:
                    new ToolkitCombinedWeatherType(this);
                    return true;
                case CustomWeatherType.Progressing:
                    if (Weathers.Count > 1)
                    {
                        new ToolkitProgressingWeatherType(this);
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }
    }
}
