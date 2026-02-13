using CombinedWeathersToolkit.Toolkit.Core;
using HarmonyLib;
using TMPro;
using WeatherRegistry;
using WeatherRegistry.Patches;
using WeatherTweaks.Definitions;

namespace CombinedWeathersToolkit.Patches
{
    [HarmonyPatch(typeof(WeatherRegistry.Managers.StartupManager))]
    internal class ScreenMapColorsArrowFixer
    {
        [HarmonyPostfix]
        [HarmonyPatch("Init")]
        public static void AddArrowDataToScreenMapColors()
        {
            foreach (var weather in WeatherManager.Weathers)
            {
                if (weather.Name.Contains('>'))
                {
                    var toolkit = ToolkitHelper.GetToolkitFromWeatherTweaksWeather(weather as WeatherTweaksWeather);
                    if (toolkit == null || toolkit.NameColor == null)
                    {
                        continue;
                    }
                    Settings.ScreenMapColors.Add(weather.Name.Replace(">", Defaults.SpecialSymbolMap[">"]), weather.ColorGradient);
                    Plugin.DebugLog($"{weather.Name} was added to ScreenMapColors by CWT using SpecialSymbolMap");
                }
            }
        }
    }


    [HarmonyPatch(typeof(SetMapScreenInfoToCurrentLevelPatch))]
    internal class CustomToolkitScreenColor
    {
        [HarmonyPostfix]
        [HarmonyPatch("GetColoredString")]
        public static void GetColoredStringToolkit(SelectableLevel level, ref string __result)
        {
            if (!Settings.ColoredWeathers)
            {
                return;
            }

            Weather currentWeather = WeatherManager.GetCurrentWeather(level);
            var toolkit = ToolkitHelper.GetToolkitFromWeatherTweaksWeather(currentWeather as WeatherTweaksWeather);

            if (toolkit == null || toolkit.NameColor == null)
            {
                return;
            }

            Plugin.DebugLog($"{toolkit.Name} weather was spawned using CWT with a defined color");

            string currentWeatherString = SetMapScreenInfoToCurrentLevelPatch.GetDisplayWeatherString(level, currentWeather);
            string currentWeatherStringOri = currentWeatherString;

            if (currentWeatherString.Contains('>'))
            {
                currentWeatherString = currentWeatherString.Replace(">", Defaults.SpecialSymbolMap[">"]);
            }

            TMP_ColorGradient? pickedColor = Settings.ScreenMapColors.TryGetValue(currentWeatherString, out TMP_ColorGradient value) ? value : null;

            if (pickedColor != null && pickedColor != ToolkitHelper.GetGradientInstance())
            {
                __result = $"<gradient={currentWeatherString}>{currentWeatherStringOri}</gradient>";
            }
            else
            {
                __result = $"{currentWeatherString}";
            }
        }
    }
}
