using CombinedWeathersToolkit.Toolkit.Core;
using WeatherTweaks.Definitions;

namespace CombinedWeathersToolkit.Toolkit
{
    internal class PredefinedRegistery
    {
        internal static void LoadAllPredefinedWeathers()
        {
            CreateWeather(new ToolkitWeather() { Name = "Majora Moon + Blood Moon" },
                "majoramoon", "bloodmoon");
        }

        private static void CreateWeather(ToolkitWeather toolkit, params string[] weathers)
        {
            CreateCombinedWeather(toolkit, weathers);
        }

        private static void CreateCombinedWeather(ToolkitWeather toolkit, params string[] weathers)
        {
            toolkit.Type = CustomWeatherType.Combined;
            RegisterWeather(toolkit, weathers);
        }

        private static void CreateProgressingWeather(ToolkitWeather toolkit, params string[] weathers)
        {
            toolkit.Type = CustomWeatherType.Progressing;
            RegisterWeather(toolkit, weathers);
        }

        private static void RegisterWeather(ToolkitWeather toolkit, params string[] weathers)
        {
            toolkit.WeightModifier = Plugin.config.PredefinedWeathersWeightModifier.Value;
            toolkit.AddWeathers(weathers);
            toolkit.Register();
        }
    }
}
