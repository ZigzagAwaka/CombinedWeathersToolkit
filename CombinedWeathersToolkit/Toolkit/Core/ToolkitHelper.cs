using BepInEx;
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
            if (toolkit.Filtering.HasValue)
                weather.Config.FilteringOption = new BooleanConfigHandler(toolkit.Filtering.Value);
            if (!toolkit.LevelFilter.IsNullOrWhiteSpace())
                weather.Config.LevelFilters = new LevelListConfigHandler(toolkit.LevelFilter);
            if (!toolkit.LevelWeights.IsNullOrWhiteSpace())
                weather.Config.LevelWeights = new LevelWeightsConfigHandler(toolkit.LevelWeights);
            if (!toolkit.WeatherToWeatherWeights.IsNullOrWhiteSpace())
                weather.Config.WeatherToWeatherWeights = new WeatherWeightsConfigHandler(toolkit.WeatherToWeatherWeights);
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
    }
}
