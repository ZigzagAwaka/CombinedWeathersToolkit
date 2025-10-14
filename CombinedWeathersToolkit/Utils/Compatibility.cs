using BepInEx.Bootstrap;

namespace CombinedWeathersToolkit.Utils
{
    internal class Compatibility
    {
        public static bool WeatherRegisteryInstalled = false;
        public static bool WeatherTweaksInstalled = false;
        public static bool LethalElementsInstalled = false;
        public static bool BiodiversityInstalled = false;
        public static bool SurfacedInstalled = false;
        public static bool PremiumScrapsInstalled = false;
        public static bool EmergencyDiceInstalled = false;
        public static bool CodeRebirthInstalled = false;
        public static bool MrovWeathersInstalled = false;
        public static bool WesleyWeathersInstalled = false;
        public static bool ImperiumInstalled = false;

        public static void CheckInstalledPlugins()
        {
            WeatherRegisteryInstalled = IsPluginInstalled("mrov.WeatherRegistry");
            WeatherTweaksInstalled = IsPluginInstalled("WeatherTweaks");
            LethalElementsInstalled = IsPluginInstalled("voxx.LethalElementsPlugin", "1.3.0");
            BiodiversityInstalled = IsPluginInstalled("com.github.biodiversitylc.Biodiversity");
            SurfacedInstalled = IsPluginInstalled("Surfaced");
            PremiumScrapsInstalled = IsPluginInstalled("zigzag.premiumscraps");
            EmergencyDiceInstalled = IsPluginInstalled("Theronguard.EmergencyDice");
            CodeRebirthInstalled = IsPluginInstalled("CodeRebirth");
            MrovWeathersInstalled = IsPluginInstalled("MrovWeathers");
            WesleyWeathersInstalled = IsWeatherBundleLoaded("Forsaken");
            ImperiumInstalled = IsPluginInstalled("giosuel.Imperium");
        }

        private static bool IsPluginInstalled(string pluginGUID, string? pluginVersion = null)
        {
            return Chainloader.PluginInfos.ContainsKey(pluginGUID) &&
                (pluginVersion == null || new System.Version(pluginVersion).CompareTo(Chainloader.PluginInfos[pluginGUID].Metadata.Version) <= 0);
        }

        private static bool IsWeatherBundleLoaded(string weatherName)
        {
            return Effects.IsModdedWeatherRegistered(weatherName);
        }
    }
}
