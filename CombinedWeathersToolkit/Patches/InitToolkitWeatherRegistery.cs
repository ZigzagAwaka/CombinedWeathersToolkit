using CombinedWeathersToolkit.Toolkit;
using HarmonyLib;
using System.Linq;
using WeatherRegistry;

namespace CombinedWeathersToolkit.Patches
{
    [HarmonyPatch(typeof(WeatherRegistry.Plugin))]
    internal class InitToolkitWeatherRegistery
    {
        [HarmonyPrefix]
        [HarmonyPatch("MainMenuInit")]
        public static void InitToolkitWeathers()
        {
            Plugin.installedWeathers.AddRange(
                WeatherManager.RegisteredWeathers.Where(w => w.Type == WeatherType.Modded && w.Origin != WeatherOrigin.WeatherTweaks)
                .Select(w => string.Concat(w.Name.Where(c => !char.IsWhiteSpace(c))).ToLower()));

            if (Plugin.config.RegisterPredefinedWeathers.Value)
                PredefinedRegistery.LoadAllPredefinedWeathers();
            if (Plugin.config.AllowConfigRegistery.Value)
                ConfigRegistery.LoadAllConfigValues();
            if (Plugin.config.AllowJsonRegistery.Value)
                JsonRegistery.LoadAllJsonFiles();
        }
    }
}
