using CombinedWeathersToolkit.Toolkit.Core;
using HarmonyLib;
using System.Linq;
using System.Reflection;
using WeatherRegistry;
using WeatherTweaks.Definitions;

namespace CombinedWeathersToolkit.Patches
{
    [HarmonyPatch(typeof(WeatherTweaksWeather))]
    internal class OverrideToolkitScrapMultipliers
    {
        [HarmonyPrefix]
        [HarmonyPatch("Init")]
        public static bool InitToolkit(WeatherTweaksWeather __instance)
        {
            if (__instance == null || __instance.Origin != WeatherOrigin.WeatherTweaks)
            {
                return true;
            }
            var toolkit = ToolkitHelper.GetToolkitFromWeatherTweaksWeather(__instance);
            if (toolkit == null)
            {
                return true;
            }
            var (value, value2) = __instance.GetDefaultMultiplierData();
            __instance.Config.ScrapValueMultiplier = new FloatConfigHandler(value);
            __instance.Config.ScrapAmountMultiplier = new FloatConfigHandler(value2);
            __instance.Effect.SunAnimatorBool = WeatherManager.GetWeather(__instance.WeatherTypes.Last().WeatherType).Effect.SunAnimatorBool;
            var methodPointer = typeof(Weather).GetMethod("Init", BindingFlags.NonPublic | BindingFlags.Instance).MethodHandle.GetFunctionPointer();
            ((System.Action)System.Activator.CreateInstance(typeof(System.Action), __instance, methodPointer)).Invoke();
            return false;
        }
    }
}
