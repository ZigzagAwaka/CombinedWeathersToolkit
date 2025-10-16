using BepInEx.Bootstrap;
using WeatherRegistry;

namespace CombinedWeathersToolkit.Utils
{
    internal class Compatibility
    {
        public static bool WeatherRegisteryInstalled = false;
        public static bool WeatherTweaksInstalled = false;
        public static bool LethalElementsInstalled = false;
        public static bool LegendWeathersInstalled = false;
        public static bool CodeRebirthInstalled = false;
        public static bool MrovWeathersInstalled = false;
        public static bool WesleyWeathersInstalled = false;
        public static bool BlueInstalled = false;
        public static bool BlackFogInstalled = false;

        public static void CheckInstalledPlugins()
        {
            WeatherRegisteryInstalled = IsPluginInstalled("mrov.WeatherRegistry");
            WeatherTweaksInstalled = IsPluginInstalled("WeatherTweaks");
            LethalElementsInstalled = IsPluginInstalled("voxx.LethalElementsPlugin", "1.3.0");
            LegendWeathersInstalled = IsPluginInstalled("zigzag.legendweathers", "2.0.0");
            CodeRebirthInstalled = IsPluginInstalled("CodeRebirth");
            MrovWeathersInstalled = IsPluginInstalled("MrovWeathers");
            CheckForLoadedWeatherBundles();
        }

        private static bool IsPluginInstalled(string pluginGUID, string? pluginVersion = null)
        {
            return Chainloader.PluginInfos.ContainsKey(pluginGUID) &&
                (pluginVersion == null || new System.Version(pluginVersion).CompareTo(Chainloader.PluginInfos[pluginGUID].Metadata.Version) <= 0);
        }

        private static void CheckForLoadedWeatherBundles()
        {
            var weathers = WeatherManager.RegisteredWeathers;
            for (int i = 0; i < weathers.Count; i++)
            {
                //Plugin.logger.LogError(weathers[i].Name);
                switch (weathers[i].Name)
                {
                    case "Forsaken":
                        WesleyWeathersInstalled = true;
                        break;
                    case "Blue":
                        BlueInstalled = true;
                        break;
                    case "Black Fog":
                        BlackFogInstalled = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
