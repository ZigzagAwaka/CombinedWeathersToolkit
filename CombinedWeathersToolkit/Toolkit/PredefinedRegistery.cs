using CombinedWeathersToolkit.Toolkit.Core;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
            RegisterWeather("Majora Eclipsed", Col("magenta"), "eclipsed", "majoramoon");
            RegisterWeather("Majora Chaos", Col("#750000"), "rainy", "stormy", "eclipsed", "majoramoon");
            RegisterWeather("Snowfall + Majora Moon", "snowfall", "majoramoon");
            RegisterWeather("Celestial Borealis", Col("#9eb5ff"), "solarflare", "majoramoon");
            RegisterWeather("Tornado + Majora Moon", "tornado", "majoramoon");
            RegisterWeather("Cosmic Crash", Col("#82ffd7"), "meteorshower", "majoramoon");
            RegisterWeather("Hallowed + Majora Moon", "hallowed", "majoramoon");
            RegisterWeather("Dark Majora Moon", Col("#290033"), "blackfog", "majoramoon");
            #endregion

            #region BLOOD MOON
            RegisterWeather("The Crimson Flood", Col("#ff4580"), "flooded", "bloodmoon");
            RegisterWeather("Snowfall + Blood Moon", "snowfall", "bloodmoon");
            RegisterWeather("Toxic Smog + Blood Moon", Col("#ff00b3"), "toxicsmog", "bloodmoon");
            RegisterWeather("Meteor Shower + Blood Moon", "meteorshower", "bloodmoon");
            RegisterWeather("Forsaken + Blood Moon", "forsaken", "bloodmoon");
            RegisterWeather("Dark Blood Moon", Col("#24000c"), "blackfog", "bloodmoon");
            RegisterWeather("Majora Supermoon", Col("#ff0095"), "majoramoon", "bloodmoon");
            #endregion

            #region LETHAL ELEMENTS
            RegisterWeather("Climate Anomaly", Col("yellow"), "snowfall", "heatwave", "solarflare");
            RegisterWeather("Heatwave + Blizzard", "heatwave", "blizzard");
            RegisterWeather("Nuclear Winter", Col("#32a89d"), "toxicsmog", "blizzard");
            RegisterWeather("Hallowed Smog", Col("#1d3d1d"), "toxicsmog", "hallowed");
            RegisterWeather("Toxic Winds", Col("#008042"), "toxicsmog", "tornado");
            RegisterWeather("Black Fog + Heatwave", "blackfog", "heatwave");
            RegisterWeather("Black Fog + Snowfall", Col("#99ffce"), "blackfog", "snowfall");
            RegisterWeather("Black Winter", Col("#5e0fb8"), "blackfog", "blizzard");
            RegisterWeather("Ashen Mist", Col("#4f2d6e"), "foggy", "toxicsmog");
            RegisterWeatherProgressing("Thermal Shift", Col("#874a00"), "blizzard", "snowfall", "none", "solarflare", "heatwave");
            RegisterWeatherProgressing("Gravitational Freeze", Col("#006887"), "heatwave", "solarflare", "none", "snowfall", "blizzard");
            #endregion

            #region CODE REBIRTH
            RegisterWeather("Foggy + Meteor Shower", Col("red"), "foggy", "meteorshower");
            RegisterWeather("Meteor Tempest", Col("#01060d"), "stormy", "tornado", "meteorshower");
            RegisterWeather("Gravity Anomaly", Col("#ff7a7a"), "meteorshower", "eclipsed", "solarflare");
            RegisterWeather("Superstorm", Col("magenta"), "stormy", "flooded", "tornado", "hurricane");
            RegisterWeather("Void Cyclone", Col("#000000"), "blackout", "tornado");
            #endregion

            #region WESLEY WEATHERS
            RegisterWeather("Flooded + Hallowed", "flooded", "hallowed");
            RegisterWeather("Hallowed Eclipse", Col("red"), "hallowed", "eclipsed");
            RegisterWeather("Forsaken Storm", Col("#94294b"), "stormy", "forsaken");
            RegisterWeather("Earthquakes + Rainy", "earthquakes", "rainy");
            RegisterWeatherProgressing("Impending Storm", Col("#c4c156"), "none", "rainy", "cloudy", "stormy", "hurricane");
            #endregion

            #region MROV WEATHERS
            RegisterWeather("Haunted", Col("#4f2d6e"), "blackout", "hallowed", "blackfog");
            RegisterWeather("Nocturne Frost", Col("##2431a6"), "blackout", "blizzard");
            RegisterWeather("Darkness", Col("#030029"), "blackfog", "blackout");
            RegisterWeather("Total Darkness", Col("#030029"), "blackfog", "blackout", "foggy");
            #endregion

            #region GENERIC MODS
            RegisterWeather("Blue Fog", Col("#060a4a"), "blue", "blackfog");
            RegisterWeather("Dark Blue Moon", Col("#004175"), "blue", "bloodmoon");
            RegisterWeather("Blue Requiem", Col("#170075"), "blue", "majoramoon");
            RegisterWeather("Eternal Darkness", Col("#030029"), "blackfog", "blackout", "foggy", "heatwave", "evilblue");
            RegisterWeather("Bloodlight Omen", "bloodmoon", "orange", "rapture");
            RegisterWeather("The Pharaoh's Curse", "dustclouds", "earthquakes", "heatwave", "rapture");
            RegisterWeather("Orange Dream", "orange", "snowfall", "solarflare");
            #endregion

            #region KENJI WEATHERS
            RegisterWeather("Majora Tempest", Col("#736f00"), "gusty", "hurricane", "majoramoon");
            RegisterWeather("Howling Winds", Col("red"), "cloudy", "gusty", "earthquakes");
            RegisterWeather("Gusty + Forsaken + Orange", "forsaken", "orange", "gusty");
            RegisterWeatherProgressing("Striking Gust", Col("#ff0000"), "gusty", "hurricane");
            #endregion

            #region CUSTOM
            RegisterWeather("The Great Flood", Col("#004175"), "flooded", "rainy");

            var weatherBlacklist = new string[] { "blizzard", "cloudy", "blackout", "blue", "evilblue", "nightshift", "orange" };
            var allWeathers = new List<string>() { "rainy", "stormy", "eclipsed" };

            foreach (var weather in Plugin.installedWeathers)  // load all installed weathers into the list
            {
                if (weatherBlacklist.Any(x => x == weather))
                    continue;
                allWeathers.Add(weather);
            }

            RegisterWeather("The End of the World", Col("magenta"), allWeathers.ToArray());
            #endregion

            Plugin.logger.LogInfo("[PredefinedRegistery] All predefined weathers are loaded");
        }

        private static void RegisterWeather(string name, params string[] weathers)
        {
            var toolkit = new ToolkitWeather() { Name = name };
            RegisterWeather(toolkit, CustomWeatherType.Combined, weathers);
        }

        private static void RegisterWeather(string name, TMP_ColorGradient color, params string[] weathers)
        {
            var toolkit = new ToolkitWeather() { Name = name, NameColor = color };
            RegisterWeather(toolkit, CustomWeatherType.Combined, weathers);
        }

        private static void RegisterWeatherProgressing(string name, params string[] weathers)
        {
            var toolkit = new ToolkitWeather() { Name = name };
            RegisterWeather(toolkit, CustomWeatherType.Progressing, weathers);
        }

        private static void RegisterWeatherProgressing(string name, TMP_ColorGradient color, params string[] weathers)
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

        private static TMP_ColorGradient Col(string colorStr)
        {
            var gradient = ToolkitHelper.GetColorGradientFromString(colorStr);
            if (gradient != null)
                return gradient;
            return new TMP_ColorGradient(Color.cyan);
        }
    }
}
