using CombinedWeathersToolkit.Toolkit.Core;
using CombinedWeathersToolkit.Utils;
using WeatherTweaks.Definitions;

namespace CombinedWeathersToolkit.Toolkit
{
    internal class PredefinedRegistery
    {
        internal static void LoadAllPredefinedWeathers()
        {
            Plugin.DebugLog("[PredefinedRegistery] Predefined weathers are enabled, now loading everything");

            if (Compatibility.LegendWeathersInstalled)
            {
                RegisterWeather("Rainy + Majora Moon", "rainy", "majoramoon");
                RegisterWeather("Stormy + Majora Moon", "stormy", "majoramoon");
                RegisterWeather("Majora Eclipsed", UnityEngine.Color.magenta, "eclipsed", "majoramoon");
                RegisterWeather("Majora Chaos", Color("#750000"), "rainy", "stormy", "eclipsed", "majoramoon");
                RegisterWeather("Flooded + Blood Moon", "flooded", "bloodmoon");
                RegisterWeather("Majora Moon + Blood Moon", "majoramoon", "bloodmoon");
                if (Compatibility.LethalElementsInstalled)
                {
                    RegisterWeather("Snowfall + Majora Moon", "snowfall", "majoramoon");
                    RegisterWeather("Solar Flare + Majora Moon", "solarflare", "majoramoon");
                    RegisterWeather("Snowfall + Blood Moon", "snowfall", "bloodmoon");
                    RegisterWeather("Toxic Smog + Blood Moon", "toxicsmog", "bloodmoon");
                }
                if (Compatibility.CodeRebirthInstalled)
                {
                    RegisterWeather("Tornado + Majora Moon", "tornado", "majoramoon");
                    RegisterWeather("Meteor Shower + Majora Moon", "meteorshower", "majoramoon");
                    RegisterWeather("Meteor Shower + Blood Moon", "meteorshower", "bloodmoon");
                }
                if (Compatibility.WesleyWeathersInstalled)
                {
                    RegisterWeather("Hallowed + Majora Moon", "hallowed", "majoramoon");
                    RegisterWeather("Majora Tempest", Color("#736f00"), "stormy", "hurricane", "majoramoon");
                    RegisterWeather("Forsaken + Blood Moon", "forsaken", "bloodmoon");
                }
                if (Compatibility.BlackFogInstalled)
                {
                    RegisterWeather("Dark Majora Moon", Color("#290033"), "blackfog", "majoramoon");
                    RegisterWeather("Dark Blood Moon", Color("#24000c"), "blackfog", "bloodmoon");
                }
            }
            if (Compatibility.LethalElementsInstalled)
            {
                RegisterWeather("Climate Anomaly", UnityEngine.Color.yellow, "snowfall", "heatwave", "solarflare");
                RegisterWeather("Heatwave + Blizzard", "heatwave", "blizzard");
                RegisterWeather("Toxic Smog + Blizzard", "toxicsmog", "blizzard");
                if (Compatibility.WesleyWeathersInstalled)
                {
                    RegisterWeather("Blizzard + Forsaken", Color("#574885"), "blizzard", "forsaken");
                    RegisterWeather("Toxic Smog + Hallowed", Color("#1d3d1d"), "toxicsmog", "hallowed");
                }
                if (Compatibility.BlackFogInstalled)
                {
                    RegisterWeather("Black Fog + Heatwave", "blackfog", "heatwave");
                    RegisterWeather("Black Fog + Snowfall", "blackfog", "snowfall");
                    RegisterWeather("Black Fog + Blizzard", "blackfog", "blizzard");
                }
                if (Compatibility.CodeRebirthInstalled)
                {
                    RegisterWeather("Toxic Storm", Color("#008042"), "toxicsmog", "tornado");
                }
            }
            if (Compatibility.CodeRebirthInstalled)
            {
                RegisterWeather("Foggy + Meteor Shower", UnityEngine.Color.red, "foggy", "meteorshower");
                RegisterWeather("Meteor Deluge", Color("#01060d"), "stormy", "flooded", "tornado", "meteorshower");
                RegisterWeather("Gravity Anomaly", Color("#ff7a7a"), "meteorshower", "eclipsed", Compatibility.LethalElementsInstalled ? "solarflare" : "");
                RegisterWeather("Superstorm", UnityEngine.Color.magenta, "stormy", "flooded", "tornado", Compatibility.WesleyWeathersInstalled ? "hurricane" : "rainy");
            }
            if (Compatibility.WesleyWeathersInstalled)
            {
                RegisterWeather("Flooded + Hallowed", "flooded", "hallowed");
                RegisterWeather("Hallowed Eclipse", UnityEngine.Color.red, "eclipsed", "hallowed");
                RegisterWeather("Forsaken Storm", Color("#94294b"), "stormy", "forsaken");
                if (Compatibility.MrovWeathersInstalled && Compatibility.BlackFogInstalled)
                {
                    RegisterWeather("Haunted", Color("#4f2d6e"), "blackout", "hallowed", "blackfog");
                }
            }
            if (Compatibility.MrovWeathersInstalled && Compatibility.BlackFogInstalled)
            {
                RegisterWeather("Darkness", UnityEngine.Color.black, "blackfog", "blackout");
                RegisterWeather("Total Darkness", UnityEngine.Color.black, "blackfog", "blackout", "foggy");
            }
            if (Compatibility.BlueInstalled)
            {
                if (Compatibility.LethalElementsInstalled)
                {
                    RegisterWeather("Bluefall", Color("#36a3d6"), "blue", "snowfall");
                }
                if (Compatibility.BlackFogInstalled)
                {
                    RegisterWeather("Blue Fog", Color("#060a4a"), "blue", "blackfog");
                }
                if (Compatibility.LegendWeathersInstalled)
                {
                    RegisterWeather("Blue Moon", Color("#004175"), "blue", "bloodmoon");
                    RegisterWeather("Majoblue", Color("#170075"), "blue", "majoramoon");
                }
            }
            RegisterWeather("The End of the World", UnityEngine.Color.magenta,
                "rainy", "stormy", "flooded",
                Compatibility.LethalElementsInstalled ? "snowfall" : "",
                Compatibility.LethalElementsInstalled ? "heatwave" : "",
                Compatibility.LethalElementsInstalled ? "solarflare" : "",
                Compatibility.LethalElementsInstalled ? "toxicsmog" : "",
                Compatibility.CodeRebirthInstalled ? "tornado" : "",
                Compatibility.CodeRebirthInstalled ? "meteorshower" : "",
                Compatibility.WesleyWeathersInstalled ? "hallowed" : "",
                Compatibility.WesleyWeathersInstalled ? "forsaken" : "",
                Compatibility.WesleyWeathersInstalled ? "hurricane" : "",
                Compatibility.BlackFogInstalled ? "blackfog" : "",
                Compatibility.LegendWeathersInstalled ? "majoramoon" : "",
                Compatibility.LegendWeathersInstalled ? "bloodmoon" : "eclipsed"
            );

            Plugin.logger.LogInfo("[PredefinedRegistery] Loaded all predefined weathers");
        }

        private static void RegisterWeather(string name, params string[] weathers)
        {
            var toolkit = new ToolkitWeather() { Name = name };
            RegisterWeather(toolkit, weathers);
        }

        private static void RegisterWeather(string name, UnityEngine.Color color, params string[] weathers)
        {
            var toolkit = new ToolkitWeather() { Name = name, NameColor = color };
            RegisterWeather(toolkit, weathers);
        }

        private static void RegisterWeather(ToolkitWeather toolkit, params string[] weathers)
        {
            toolkit.Type = CustomWeatherType.Combined;
            toolkit.WeightModifier = Plugin.config.PredefinedWeathersWeightModifier.Value;
            toolkit.AddWeathers(weathers);
            if (toolkit.Register())
                Plugin.DebugLog($"[PredefinedRegistery] Registered weather: {toolkit.Name}");
        }

        private static UnityEngine.Color Color(string hexColor)
        {
            if (UnityEngine.ColorUtility.TryParseHtmlString(hexColor, out var customColor))
                return customColor;
            return UnityEngine.Color.white;
        }
    }
}
