using CombinedWeathersToolkit.Toolkit.Core;
using HarmonyLib;
using TMPro;
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
            var toolkit = ToolkitHelper.GetToolkitFromWeatherTweaksWeather(currentWeather as WeatherTweaksWeather);
            if (toolkit == null || toolkit.NameColor == null)
            {
                return;
            }
            TMP_ColorGradient color = toolkit.NameColor;
            Plugin.DebugLog($"{toolkit.Name} weather was spawned using CWT with a defined color of {color.topLeft},{color.topRight},{color.bottomLeft},{color.bottomRight}");
            string currentWeatherString = SetMapScreenInfoToCurrentLevelPatch.GetDisplayWeatherString(level, currentWeather);
            TMP_ColorGradient pickedColor = Settings.ScreenMapColors.TryGetValue(currentWeatherString, out TMP_ColorGradient value)
               ? value
               : new TMP_ColorGradient();

            //color.topLeft = new Color(color.topLeft.r, color.topLeft.g * 1.1f, color.topLeft.b, color.topLeft.a);
            //color.topRight = new Color(color.topRight.r, color.topRight.g * 1.1f, color.topRight.b, color.topRight.a);
            //color.bottomLeft = new Color(color.bottomLeft.r, color.bottomLeft.g * 1.1f, color.bottomLeft.b, color.bottomLeft.a);
            //color.bottomRight = new Color(color.bottomRight.r, color.bottomRight.g * 1.1f, color.bottomRight.b, color.bottomRight.a);
            //color = new Color(color.r, color.g * 1.1f, color.b, color.a);
            //var colorValue = ColorUtility.ToHtmlStringRGB(color);
            if (pickedColor != null && pickedColor != new TMP_ColorGradient())
            {
                __result = $"<gradient={currentWeatherString}>{currentWeatherString}</gradient>";
            }
            else
            {
                __result = $"{currentWeatherString}";
            }
        }
    }
}
