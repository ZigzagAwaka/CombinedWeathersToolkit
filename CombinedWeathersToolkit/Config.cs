using BepInEx.Configuration;
using CombinedWeathersToolkit.Utils;

namespace CombinedWeathersToolkit
{
    class Config
    {
        public readonly ConfigEntry<bool> RegisterPredefinedCombinedWeathers;
        public readonly ConfigEntry<bool> AllowConfigRegistery;
        public readonly ConfigEntry<bool> AllowJsonRegistery;

        public Config(ConfigFile cfg)
        {
            cfg.SaveOnConfigSet = false;

            RegisterPredefinedCombinedWeathers = cfg.Bind("General", "Register predefined combined weathers", true, "If true, some predefined combined weather effects will be registered by this mod without the need of manually creating them.");
            AllowConfigRegistery = cfg.Bind("General", "Allow config registery", true, "Allow the mod to register any combined weather effects that are created in the config.");
            AllowJsonRegistery = cfg.Bind("General", "Allow json registery", true, "Allow the mod to register any combined weather effects that are created with .json files.");

            cfg.Save();
            cfg.SaveOnConfigSet = true;
        }

        public void SetupCustomConfigs()
        {
            Compatibility.CheckInstalledPlugins();
            if (!Compatibility.WeatherRegisteryInstalled)
                Plugin.logger.LogError("WeatherRegistery is not installed! Please install WeatherRegistery before using this mod.");
            if (!Compatibility.WeatherTweaksInstalled)
                Plugin.logger.LogError("WeatherTweaks is not installed! Please install WeatherTweaks before using this mod.");
        }
    }
}
