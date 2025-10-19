using BepInEx.Bootstrap;

namespace CombinedWeathersToolkit.Utils
{
    internal class Compatibility
    {
        public static bool WeatherRegisteryInstalled = false;
        public static bool WeatherTweaksInstalled = false;

        public static void CheckInstalledPlugins()
        {
            WeatherRegisteryInstalled = IsPluginInstalled("mrov.WeatherRegistry");
            WeatherTweaksInstalled = IsPluginInstalled("WeatherTweaks");
        }

        private static bool IsPluginInstalled(string pluginGUID, string? pluginVersion = null)
        {
            return Chainloader.PluginInfos.ContainsKey(pluginGUID) &&
                (pluginVersion == null || new System.Version(pluginVersion).CompareTo(Chainloader.PluginInfos[pluginGUID].Metadata.Version) <= 0);
        }
    }
}
