using CombinedWeathersToolkit.Toolkit.Core;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using WeatherRegistry.Utils;
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
            RegisterWeather("Majora Eclipsed", Col("#ff0000/#ff59ff/#f2006d/#ff59ff"), "eclipsed", "majoramoon");
            RegisterWeather("Majora Chaos", Col("#aa0000/#aa0000/#f2006d/#f2006d"), "rainy", "stormy", "eclipsed", "majoramoon");
            RegisterWeather("Snowfall + Majora Moon", "snowfall", "majoramoon");
            RegisterWeather("Celestial Borealis", Col("#ff80ff/#80ffff/#f2006d/#80ffff"), "solarflare", "majoramoon");
            RegisterWeather("Tornado + Majora Moon", "tornado", "majoramoon");
            RegisterWeather("Cosmic Crash", Col("#82ffd7/red/red/red"), "meteorshower", "majoramoon");
            RegisterWeather("Hallowed + Majora Moon", "hallowed", "majoramoon");
            RegisterWeather("Dark Majora Moon", Col("black/magenta/magenta/black"), "blackfog", "majoramoon");
            #endregion

            #region BLOOD MOON
            RegisterWeather("The Crimson Flood", Col("#ff4580"), "flooded", "bloodmoon");
            RegisterWeather("Snowfall + Blood Moon", "snowfall", "bloodmoon");
            RegisterWeather("Toxic Smog + Blood Moon", Col("#ff4580/#ff4580/#00b000/#00b000"), "toxicsmog", "bloodmoon");
            RegisterWeather("Meteor Shower + Blood Moon", Col("#ff4580/#ff4580/#4d0011/#4d0011"), "meteorshower", "bloodmoon");
            RegisterWeather("Forsaken + Blood Moon", "forsaken", "bloodmoon");
            RegisterWeather("Dark Blood Moon", Col("#ff4580/#ff4580/#ff4580/black"), "blackfog", "bloodmoon");
            RegisterWeather("Majora Supermoon", Col("#ff0095/magenta/#ff0095/#ff0095"), "majoramoon", "bloodmoon");
            #endregion

            #region LETHAL ELEMENTS
            RegisterWeather("Climate Anomaly", Col("yellow"), "snowfall", "heatwave", "solarflare");
            RegisterWeather("Heatwave + Blizzard", "heatwave", "blizzard");
            RegisterWeather("Nuclear Winter", Col("#04aca4/#069983/#0eb157/white"), "toxicsmog", "blizzard");
            RegisterWeather("Hallowed Smog", Col("#fc7714/#0abc63/#fc7714/#0abc63"), "toxicsmog", "hallowed");
            RegisterWeather("Toxic Winds", Col("#008042"), "toxicsmog", "tornado");
            RegisterWeather("Black Fog + Heatwave", "blackfog", "heatwave");
            RegisterWeather("Black Fog + Snowfall", Col("#99ffce"), "blackfog", "snowfall");
            RegisterWeather("Black Winter", Col("#6ba6fe/#1b7deb/#307cb6/black"), "blackfog", "blizzard");
            RegisterWeather("Ashen Mist", Col("#4f2d6e"), "foggy", "toxicsmog");
            RegisterWeatherProgressing("Thermal Shift", Col("#874a00"), "blizzard", "snowfall", "none", "solarflare", "heatwave");
            RegisterWeatherProgressing("Gravitational Freeze", Col("#006887"), "heatwave", "solarflare", "none", "snowfall", "blizzard");
            #endregion

            #region CODE REBIRTH
            RegisterWeather("Foggy + Meteor Shower", Col("red"), "foggy", "meteorshower");
            RegisterWeather("Meteor Tempest", Col("red/yellow/#ff5300/yellow"), "stormy", "tornado", "meteorshower");
            RegisterWeather("Gravity Anomaly", Col("cyan/#ff5300/yellow/red"), "meteorshower", "eclipsed", "solarflare");
            RegisterWeather("Superstorm", Col("magenta"), "stormy", "flooded", "tornado", "hurricane");
            RegisterWeather("Void Cyclone", Col("white/white/#800000/#800000"), "blackout", "tornado");
            #endregion

            #region WESLEY WEATHERS
            RegisterWeather("Flooded + Hallowed", "flooded", "hallowed");
            RegisterWeather("Hallowed Eclipse", Col("red"), "hallowed", "eclipsed");
            RegisterWeather("Forsaken Storm", Col("#ff0080/#94294b/#ffb164/#ffb164"), "stormy", "forsaken");
            RegisterWeather("Rainy + Earthquakes + Solar Flare", "rainy", "earthquakes", "solarflare");
            RegisterWeatherProgressing("Impending Storm", Col("#c4c156"), "none", "rainy", "cloudy", "stormy", "hurricane");
            #endregion

            #region MROV WEATHERS
            RegisterWeather("Haunted", Col("#ffb164/#ffb164/#8000ff/#d3a70c"), "blackout", "hallowed", "blackfog");
            RegisterWeather("Nocturne Frost", Col("#2431a6"), "blackout", "blizzard");
            RegisterWeather("Darkness", Col("black/black/white/white"), "blackfog", "blackout");
            RegisterWeather("Total Darkness", Col("#9680ff/white/#050066/#ba8df5"), "blackfog", "blackout", "foggy");
            #endregion

            #region GENERIC MODS
            RegisterWeather("Blue Fog", Col("#0714e3"), "blue", "blackfog");
            RegisterWeather("Dark Blue Moon", Col("#ff0080/black/#4553f8/#4553f8"), "blue", "bloodmoon");
            RegisterWeather("Blue Requiem", Col("#0080c0/#0080c0/#ff00ff/#ff00ff"), "blue", "majoramoon");
            RegisterWeather("Eternal Darkness", Col("black/cyan/black/blue"), "blackfog", "blackout", "foggy", "heatwave", "evilblue");
            RegisterWeather("Bloodlight Omen", Col("#ff0080/#ff0080/#ffb164/#ffcc00"), "bloodmoon", "orange", "rapture");
            RegisterWeather("The Pharaoh's Curse", Col("#ffcc00/#ffcc00/red/#ffcc00"), "dustclouds", "earthquakes", "heatwave", "rapture");
            RegisterWeather("Orange Dream", Col("#ffb164"), "orange", "snowfall", "solarflare");
            #endregion

            #region KENJI WEATHERS
            RegisterWeather("Majora Tempest", Col("#18e7cd/#91ff7d/#efa4ff/#00849b"), "gusty", "hurricane", "majoramoon");
            RegisterWeather("Howling Winds", Col("#b0ff66"), "cloudy", "gusty", "earthquakes");
            RegisterWeather("Gusty + Forsaken + Orange", "forsaken", "orange", "gusty");
            RegisterWeatherProgressing("Striking Gust", Col("#3ffe9e/#707be9/#3ffe9e/#707be9"), "gusty", "hurricane");
            #endregion

            #region CUSTOM
            RegisterWeather("The Great Flood", Col("#2b79ff"), "flooded", "rainy");

            var weatherBlacklist = new string[] { "blizzard", "cloudy", "blackout", "blue", "evilblue", "nightshift", "orange" };
            var allWeathers = new List<string>() { "rainy", "stormy", "eclipsed" };

            foreach (var weather in Plugin.installedWeathers)  // load all installed weathers into the list
            {
                if (weatherBlacklist.Any(x => x == weather))
                    continue;
                allWeathers.Add(weather);
            }

            RegisterWeather("The End of the World", Col("magenta/red/#ffb164/cyan"), allWeathers.ToArray());
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
            return ColorConverter.ToTMPColorGradient(Color.cyan);
        }
    }
}
