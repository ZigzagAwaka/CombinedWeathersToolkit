using System.Collections.Generic;
using WeatherRegistry;
using WeatherTweaks.Definitions;

namespace CombinedWeathersToolkit.Toolkit.Core
{
    internal class ToolkitHelper
    {
        internal static string GetConfigCategory(ToolkitWeather toolkit)
        {
            return "WeatherToolkit Weather: " + toolkit.Name;
        }

        internal static void SetColor(ToolkitWeather toolkit, Weather weather)
        {
            if (toolkit.NameColor.HasValue)
                weather.Color = toolkit.NameColor.Value;
        }

        internal static void SetConfig(ToolkitWeather toolkit, WeatherTweaksWeather weather)
        {
            weather.Config.FilteringOption = new BooleanConfigHandler(toolkit.Filtering ?? false);
            weather.Config.LevelFilters = new LevelListConfigHandler(!string.IsNullOrEmpty(toolkit.LevelFilter) ? toolkit.LevelFilter : "Company");
            weather.Config.LevelWeights = new LevelWeightsConfigHandler(!string.IsNullOrEmpty(toolkit.LevelWeights) ? toolkit.LevelWeights : "");
            weather.Config.WeatherToWeatherWeights = new WeatherWeightsConfigHandler(!string.IsNullOrEmpty(toolkit.WeatherToWeatherWeights) ? toolkit.WeatherToWeatherWeights : "");
        }

        internal static (float valueMultiplier, float amountMultiplier) GetMultipliers(ToolkitWeather toolkit)
        {
            var (value, amount) = (1f, 1f);
            if (toolkit.ScrapValueMultiplier.HasValue)
                value = toolkit.ScrapValueMultiplier.Value;
            if (toolkit.ScrapAmountMultiplier.HasValue)
                amount = toolkit.ScrapAmountMultiplier.Value;
            return (value, amount);
        }

        internal static List<ProgressingWeatherEntry> GetProgressingWeatherEntries(ToolkitWeather toolkit)
        {
            var result = new List<ProgressingWeatherEntry>();
            for (int i = 1; i < toolkit.Weathers.Count; i++)
            {
                result.Add(new ProgressingWeatherEntry() { DayTime = toolkit.ProgressingTimes[i - 1], Chance = toolkit.ProgressingChances[i - 1], Weather = toolkit.Weathers[i] });
            }
            return result;
        }

        public static ToolkitWeather? GetToolkitFromWeatherTweaksWeather(WeatherTweaksWeather? weather)
        {
            if (weather == null || !weather.ConfigCategory.StartsWith("WeatherToolkit"))
                return null;
            ToolkitWeather? toolkit = null;
            var currentWeatherAsToolkitCombined = weather as ToolkitCombinedWeatherType;
            if (currentWeatherAsToolkitCombined != null)
                toolkit = currentWeatherAsToolkitCombined.ToolkitWeather;
            else
            {
                var currentWeatherAsToolkitProgressing = weather as ToolkitProgressingWeatherType;
                if (currentWeatherAsToolkitProgressing != null)
                    toolkit = currentWeatherAsToolkitProgressing.ToolkitWeather;
            }
            return toolkit;
        }
    }
}
