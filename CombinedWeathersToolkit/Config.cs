using BepInEx.Configuration;
using CombinedWeathersToolkit.Utils;
using System.Collections.Generic;
using System.Linq;

namespace CombinedWeathersToolkit
{
    class Config
    {
        public readonly ConfigEntry<bool> AllowConfigRegistery;
        public readonly ConfigEntry<bool> AllowJsonRegistery;
        public readonly ConfigEntry<bool> RegisterPredefinedWeathers;
        public readonly ConfigEntry<float> PredefinedWeathersWeightModifier;
        public readonly ConfigEntry<string> WeatherConfigCreatorString;
        public readonly ConfigEntry<string> WeatherConfigCreatorExample;
        public readonly ConfigEntry<bool> DebugLogsEnabled;
        public readonly ConfigEntry<bool> DebugCommandsEnabled;
        public readonly List<string> WeatherConfigCreatorList = new List<string>();

        public Config(ConfigFile cfg)
        {
            cfg.SaveOnConfigSet = false;

            AllowConfigRegistery = cfg.Bind("General", "Allow config registery", true, "Allow the mod to register any combined weather effects that are created in the config.");
            AllowJsonRegistery = cfg.Bind("General", "Allow json registery", true, "Allow the mod to register any combined weather effects that are created with .json files.");
            RegisterPredefinedWeathers = cfg.Bind("Predefined weathers", "Register predefined combined weathers", true, "If true, some predefined combined weather effects will be registered by this mod without the need of manually creating them.");
            PredefinedWeathersWeightModifier = cfg.Bind("Predefined weathers", "Global weight modifier", 0.2f, "If 'Register predefined combined weathers' is true, this config will dynamically update the weight of all predefined combined weathers based on this value which will act as a multiplier.\nIf you want all predefined weathers to spawn more often then enter a bigger value here (something like 1 or 2).");
            DebugLogsEnabled = cfg.Bind("Debug", "Debug logs", false, "Enable more explicit logs in the console (for debug reasons).");
            DebugCommandsEnabled = cfg.Bind("Debug", "Debug commands", false, "Enable debug commands in the game's chat (for debug reasons).");
            WeatherConfigCreatorString = cfg.Bind("Config weathers", "Weather Config creator", "", "Comma separated list of combined weather effects that will be used to register new weathers from the config.");
            WeatherConfigCreatorExample = cfg.Bind("Config weathers", "Example", "Eclipsed + Foggy : Eclipsed : Foggy,Solar Storm: color(#ff0000) : solarflare : stormy", "This is an example config that explains how to register new combined weathers with the 'Weather Config creator' config.\nFollow the format in the example to write your own weather in 'Weather Config creator'.");

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
            if (AllowConfigRegistery.Value)
                PopulateWeatherConfigCreatorList();
        }

        private void PopulateWeatherConfigCreatorList()
        {
            if (string.IsNullOrWhiteSpace(WeatherConfigCreatorString.Value))
                return;
            foreach (string value in WeatherConfigCreatorString.Value.Split(',').Select(s => s.Trim()))
            {
                WeatherConfigCreatorList.Add(value);
            }
        }
    }
}
