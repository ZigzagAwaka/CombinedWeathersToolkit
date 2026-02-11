using System.Linq;
using TMPro;
using UnityEngine;
using WeatherRegistry;
using WeatherRegistry.Utils;
using WeatherTweaks.Definitions;

namespace CombinedWeathersToolkit.Toolkit.Core
{
    internal class ToolkitHelper
    {
        internal static string GetConfigCategory(ToolkitWeather toolkit)
        {
            return "WeatherToolkit Weather: " + toolkit.Name;
        }

        internal static void SetColor(ToolkitWeather toolkit, Weather weather)
        {
            if (toolkit.NameColor != null)
                weather.ColorGradient = toolkit.NameColor;
        }

        internal static void SetConfig(ToolkitWeather toolkit, WeatherTweaksWeather weather)
        {
            weather.Config.FilteringOption = new BooleanConfigHandler(toolkit.Filtering ?? false);
            weather.Config.LevelFilters = new LevelListConfigHandler(!string.IsNullOrEmpty(toolkit.LevelFilter) ? toolkit.LevelFilter : "Company");
            weather.Config.LevelWeights = new LevelWeightsConfigHandler(!string.IsNullOrEmpty(toolkit.LevelWeights) ? toolkit.LevelWeights : "");
            weather.Config.WeatherToWeatherWeights = new WeatherWeightsConfigHandler(!string.IsNullOrEmpty(toolkit.WeatherToWeatherWeights) ? toolkit.WeatherToWeatherWeights : "");
        }

        internal static (float valueMultiplier, float amountMultiplier) GetMultipliers(ToolkitWeather toolkit)
        {
            var (value, amount) = (1f, 1f);
            if (toolkit.ScrapValueMultiplier.HasValue)
                value = toolkit.ScrapValueMultiplier.Value;
            if (toolkit.ScrapAmountMultiplier.HasValue)
                amount = toolkit.ScrapAmountMultiplier.Value;
            return (value, amount);
        }

        internal static bool ShouldResetAmountData(ToolkitWeather toolkit)
        {
            return toolkit.Name == "The End of the World";
        }

        public static ToolkitWeather? GetToolkitFromWeatherTweaksWeather(WeatherTweaksWeather? weather)
        {
            if (weather == null || !weather.ConfigCategory.StartsWith("WeatherToolkit"))
                return null;
            ToolkitWeather? toolkit = null;
            if (weather is ToolkitCombinedWeatherType currentWeatherAsToolkitCombined)
                toolkit = currentWeatherAsToolkitCombined.ToolkitWeather;
            else
            {
                if (weather is ToolkitProgressingWeatherType currentWeatherAsToolkitProgressing)
                    toolkit = currentWeatherAsToolkitProgressing.ToolkitWeather;
            }
            return toolkit;
        }


        public static Color[] GetColorsFromString(string colorString)
        {
            var parsedColors = colorString.ToLower().Split('/').Select(s => s.Trim()).ToArray();
            Color[] colors = new Color[parsedColors.Length];

            for (int i = 0; i < parsedColors.Length; i++)
            {
                var colorStr = parsedColors[i];
                Color color;

                if (colorStr[0] == '#')
                {
                    if (ColorUtility.TryParseHtmlString(colorStr, out var customColor))
                        color = customColor;
                    else
                        color = Color.cyan;
                }
                else
                {
                    color = colorStr.ToLower() switch
                    {
                        "red" => Color.red,
                        "green" => Color.green,
                        "blue" => Color.blue,
                        "yellow" => Color.yellow,
                        "cyan" => Color.cyan,
                        "magenta" => Color.magenta,
                        "black" => Color.black,
                        "white" => Color.white,
                        "gray" => Color.gray,
                        "grey" => Color.grey,
                        "clear" => Color.clear,
                        _ => Color.cyan
                    };
                }
                colors[i] = color;
            }
            return colors;
        }


        public static TMP_ColorGradient? GetColorGradientFromString(string colorString)
        {
            Color[] colors = GetColorsFromString(colorString);
            return colors.Length switch
            {
                1 => ColorConverter.ToTMPColorGradient(colors[0]),
                2 => GetGradientFull(colors[0], colors[1], colors[0], colors[1]),
                3 => GetGradientFull(colors[0], colors[1], colors[2], colors[2]),
                4 => GetGradientFull(colors[0], colors[1], colors[2], colors[3]),
                _ => null,
            };
        }

        private static TMP_ColorGradient GetGradientFull(Color color0, Color color1, Color color2, Color color3)
        {
            TMP_ColorGradient colorGradient = ColorConverter.CreateColorGradientInstance();
            colorGradient.colorMode = ColorMode.FourCornersGradient;
            colorGradient.topLeft = color0;
            colorGradient.topRight = color1;
            colorGradient.bottomLeft = color2;
            colorGradient.bottomRight = color3;
            return colorGradient;
        }
    }
}