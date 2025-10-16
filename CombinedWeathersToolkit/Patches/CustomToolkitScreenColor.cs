using CombinedWeathersToolkit.Toolkit.Core;
using HarmonyLib;
using UnityEngine;
using WeatherRegistry;
using WeatherRegistry.Patches;
using WeatherTweaks.Definitions;

namespace CombinedWeathersToolkit.Patches
{
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
            var currentWeatherAsWeatherTweaks = currentWeather as WeatherTweaksWeather;
            if (currentWeatherAsWeatherTweaks == null || !currentWeatherAsWeatherTweaks.ConfigCategory.StartsWith("WeatherToolkit"))
            {
                return;
            }
            ToolkitWeather? toolkit = null;
            var currentWeatherAsToolkitCombined = currentWeatherAsWeatherTweaks as ToolkitCombinedWeatherType;
            if (currentWeatherAsToolkitCombined != null)
                toolkit = currentWeatherAsToolkitCombined.ToolkitWeather;
            else
            {
                var currentWeatherAsToolkitProgressing = currentWeatherAsWeatherTweaks as ToolkitProgressingWeatherType;
                if (currentWeatherAsToolkitProgressing != null)
                    toolkit = currentWeatherAsToolkitProgressing.ToolkitWeather;
            }
            if (toolkit == null || !toolkit.NameColor.HasValue)
            {
                return;
            }
            Plugin.DebugLog($"{toolkit.Name} weather was spawned using CWT with a defined color of {toolkit.NameColor}");
            string currentWeatherString = SetMapScreenInfoToCurrentLevelPatch.GetDisplayWeatherString(level, currentWeather);
            var color = toolkit.NameColor.Value;
            color = new Color(color.r, color.g * 1.1f, color.b, color.a);
            var colorValue = ColorUtility.ToHtmlStringRGB(color);
            __result = $"<color=#{colorValue}>{currentWeatherString}</color>";
        }
    }
}
