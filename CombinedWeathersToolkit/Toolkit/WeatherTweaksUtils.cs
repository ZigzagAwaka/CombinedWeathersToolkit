using System.Linq;
using UnityEngine;
using WeatherRegistry;
using WeatherTweaks.Definitions;

namespace CombinedWeathersToolkit.Toolkit
{
    public class WeatherTweaksUtils
    {
        public static void RegisterCombinedWeather(string name, params WeatherResolvable[] weathers)
        {
            new CombinedWeatherType(name, weathers.ToList());
        }

        public static void RegisterCombinedWeather(string name, Color nameColor, params WeatherResolvable[] weathers)
        {
            new CombinedWeatherType(name, weathers.ToList())
            {
                Color = nameColor
            };
        }
    }
}
