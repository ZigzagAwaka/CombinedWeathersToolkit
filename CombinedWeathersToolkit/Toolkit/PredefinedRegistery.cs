using CombinedWeathersToolkit.Toolkit.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WeatherTweaks.Definitions;

namespace CombinedWeathersToolkit.Toolkit
{
    internal class PredefinedRegistery
    {
        internal static void LoadAllPredefinedWeathers()
        {
            Plugin.DebugLog("[PredefinedRegistery] Predefined weathers are enabled, now loading everything");

            #region MAJORA MOON
            RegisterWeather("Rainy + Majora Moon", "rainy", "majoramoon");
            RegisterWeather("Majora Eclipsed", Color.magenta, "eclipsed", "majoramoon");
            RegisterWeather("Majora Chaos", Hex("#750000"), "rainy", "stormy", "eclipsed", "majoramoon");
            RegisterWeather("Snowfall + Majora Moon", "snowfall", "majoramoon");
            RegisterWeather("Majora Aurora", Hex("#9eb5ff"), "solarflare", "majoramoon");
            RegisterWeather("Tornado + Majora Moon", "tornado", "majoramoon");
            RegisterWeather("Cosmic Crash", Hex("#82ffd7"), "meteorshower", "majoramoon");
            RegisterWeather("Hallowed + Majora Moon", "hallowed", "majoramoon");
            RegisterWeather("Majora Tempest", Hex("#736f00"), "stormy", "hurricane", "majoramoon");
            RegisterWeather("Dark Majora Moon", Hex("#290033"), "blackfog", "majoramoon");
            #endregion

            #region BLOOD MOON
            RegisterWeather("The Crimson Flood", Hex("#ff4580"), "flooded", "bloodmoon");
            RegisterWeather("Snowfall + Blood Moon", "snowfall", "bloodmoon");
            RegisterWeather("Toxic Smog + Blood Moon", Hex("#ff00b3"), "toxicsmog", "bloodmoon");
            RegisterWeather("Meteor Shower + Blood Moon", "meteorshower", "bloodmoon");
            RegisterWeather("Forsaken + Blood Moon", "forsaken", "bloodmoon");
            RegisterWeather("Dark Blood Moon", Hex("#24000c"), "blackfog", "bloodmoon");
            RegisterWeather("Majora Supermoon", Hex("#ff0095"), "majoramoon", "bloodmoon");
            #endregion

            #region LETHAL ELEMENTS
            RegisterWeather("Climate Anomaly", Color.yellow, "snowfall", "heatwave", "solarflare");
            RegisterWeather("Heatwave + Blizzard", "heatwave", "blizzard");
            RegisterWeather("Nuclear Winter", Hex("#32a89d"), "toxicsmog", "blizzard");
            RegisterWeather("Blizzard + Forsaken", Hex("#574885"), "blizzard", "forsaken");
            RegisterWeather("Hallowed Smog", Hex("#1d3d1d"), "toxicsmog", "hallowed");
            RegisterWeather("Toxic Winds", Hex("#008042"), "toxicsmog", "tornado");
            RegisterWeather("Black Fog + Heatwave", "blackfog", "heatwave");
            RegisterWeather("Black Fog + Snowfall", Hex("#99ffce"), "blackfog", "snowfall");
            RegisterWeather("Black Winter", Hex("#5e0fb8"), "blackfog", "blizzard");
            RegisterWeatherProgressing("Thermal Shift", Hex("#874a00"), "blizzard", "snowfall", "none", "solarflare", "heatwave");
            RegisterWeatherProgressing("Gravitational Freeze", Hex("#006887"), "heatwave", "solarflare", "none", "snowfall", "blizzard");
            #endregion

            #region CODE REBIRTH
            RegisterWeather("Foggy + Meteor Shower", Color.red, "foggy", "meteorshower");
            RegisterWeather("Meteor Tempest", Hex("#01060d"), "stormy", "tornado", "meteorshower");
            RegisterWeather("Gravity Anomaly", Hex("#ff7a7a"), "meteorshower", "eclipsed", "solarflare");
            RegisterWeather("Superstorm", Color.magenta, "stormy", "flooded", "tornado", "hurricane");
            #endregion

            #region WESLEY WEATHERS
            RegisterWeather("Flooded + Hallowed", "flooded", "hallowed");
            RegisterWeather("Hallowed Eclipse", Color.red, "hallowed", "eclipsed");
            RegisterWeather("Forsaken Storm", Hex("#94294b"), "stormy", "forsaken");
            RegisterWeatherProgressing("Impending Storm", Hex("#c4c156"), "none", "rainy", "cloudy", "stormy", "hurricane");
            #endregion

            #region MROV WEATHERS
            RegisterWeather("Haunted", Hex("#4f2d6e"), "blackout", "hallowed", "blackfog");
            RegisterWeather("Nocturne Frost", Hex("##2431a6"), "blackout", "blizzard");
            RegisterWeather("Darkness", Hex("#030029"), "blackfog", "blackout");
            RegisterWeather("Total Darkness", Hex("#030029"), "blackfog", "blackout", "foggy");
            #endregion

            #region BLUE
            RegisterWeather("Bluefall", Hex("#36a3d6"), "blue", "snowfall");
            RegisterWeather("Blue Fog", Hex("#060a4a"), "blue", "blackfog");
            RegisterWeather("Blue Moon", Hex("#004175"), "blue", "bloodmoon");
            RegisterWeather("Majoblue", Hex("#170075"), "blue", "majoramoon");
            RegisterWeather("Eternal Darkness", Hex("#030029"), "blackfog", "blackout", "foggy", "heatwave", "evilblue");
            #endregion

            #region CUSTOM
            var weatherBlacklist = new string[] { "blizzard", "cloudy", "blackout", "blue", "evilblue", "nightshift" };
            var allWeathers = new List<string>() { "rainy", "stormy", "eclipsed" };

            foreach (var weather in Plugin.installedWeathers)  // load all installed weathers into the list
            {
                if (weatherBlacklist.Any(x => x == weather))
                    continue;
                allWeathers.Add(weather);
            }

            RegisterWeather("The End of the World", Color.magenta, allWeathers.ToArray());
            #endregion

            Plugin.logger.LogInfo("[PredefinedRegistery] All predefined weathers are loaded");
        }

        private static void RegisterWeather(string name, params string[] weathers)
        {
            var toolkit = new ToolkitWeather() { Name = name };
            RegisterWeather(toolkit, CustomWeatherType.Combined, weathers);
        }

        private static void RegisterWeather(string name, Color color, params string[] weathers)
        {
            var toolkit = new ToolkitWeather() { Name = name, NameColor = color };
            RegisterWeather(toolkit, CustomWeatherType.Combined, weathers);
        }

        private static void RegisterWeatherProgressing(string name, params string[] weathers)
        {
            var toolkit = new ToolkitWeather() { Name = name };
            RegisterWeather(toolkit, CustomWeatherType.Progressing, weathers);
        }

        private static void RegisterWeatherProgressing(string name, Color color, params string[] weathers)
        {
            var toolkit = new ToolkitWeather() { Name = name, NameColor = color };
            RegisterWeather(toolkit, CustomWeatherType.Progressing, weathers);
        }

        private static void RegisterWeather(ToolkitWeather toolkit, CustomWeatherType type, params string[] weathers)
        {
            toolkit.Type = type;
            toolkit.WeightModifier = Plugin.config.PredefinedWeathersWeightModifier.Value;
            toolkit.AddWeathers(weathers);
            if (toolkit.Register())
                Plugin.DebugLog($"[PredefinedRegistery] Registered weather: {toolkit.Name}");
        }

        private static Color Hex(string hexColor)
        {
            if (ColorUtility.TryParseHtmlString(hexColor, out var customColor))
                return customColor;
            return Color.cyan;
        }
    }
}
