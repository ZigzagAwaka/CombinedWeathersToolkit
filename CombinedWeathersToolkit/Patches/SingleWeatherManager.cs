using CombinedWeathersToolkit.Utils;
using HarmonyLib;
using System.Collections.Generic;
using WeatherRegistry;
using WeatherTweaks;
using static WeatherTweaks.WeatherCalculation;

namespace CombinedWeathersToolkit.Patches
{
    [HarmonyPatch(typeof(WeatherTweaksWeatherAlgorithm))]
    internal class SingleWeatherManager
    {
        [HarmonyPrefix]
        [HarmonyPatch("SelectWeathers")]
        public static bool OverrideCalculatedWeather(ref WeatherTweaksWeatherAlgorithm __instance, ref Dictionary<SelectableLevel, LevelWeatherType> __result, StartOfRound startOfRound)
        {
            if (string.IsNullOrEmpty(Plugin.config.UniqueWeatherAlgorithm.Value) || !StartOfRound.Instance.IsHost)
            {
                return true;
            }

            previousDayWeather.Clear();

            if (startOfRound.gameStats.daysSpent == 0 && WeatherTweaks.ConfigManager.FirstDaySpecial.Value)
            {
                return true;
            }

            Dictionary<SelectableLevel, Weather> currentWeather = new Dictionary<SelectableLevel, Weather>();
            List<SelectableLevel> levels = Variables.GetGameLevels();

            foreach (SelectableLevel level in levels)
            {
                previousDayWeather[level.PlanetName] = level.currentWeather;

                Weather? weather = Effects.GetWeather(Plugin.config.UniqueWeatherAlgorithm.Value);

                if (weather == null)
                {
                    Plugin.logger.LogError("The weather in the UniqueWeatherAlgorithm config is not valid, reverting to WeatherTweaks algorithm");
                    return true;
                }

                currentWeather[level] = weather;
            }

            Dictionary<SelectableLevel, LevelWeatherType> selectedWeathersLevel = new Dictionary<SelectableLevel, LevelWeatherType>();

            foreach (var selectedWeather in currentWeather)
            {
                selectedWeathersLevel[selectedWeather.Key] = selectedWeather.Value.VanillaWeatherType;
            }

            __result = selectedWeathersLevel;

            return false;
        }
    }
}
