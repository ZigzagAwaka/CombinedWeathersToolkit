using WeatherRegistry;

namespace CombinedWeathersToolkit.Utils
{
    internal class Effects
    {
        public static void ChangeWeather(string weatherNameResolvable)
        {
            ChangeWeather(new WeatherNameResolvable(weatherNameResolvable).WeatherType);
        }

        public static void ChangeWeather(LevelWeatherType weather)
        {
            StartOfRound.Instance.currentLevel.currentWeather = weather;
            if (GameNetworkManager.Instance.localPlayerController.IsHost)
                WeatherController.SetWeatherEffects(weather);
        }

        public static void AddCombinedWeather(string weatherNameResolvable)
        {
            var weatherType = weatherNameResolvable.ToLower() switch
            {
                "rainy" => LevelWeatherType.Rainy,
                "stormy" => LevelWeatherType.Stormy,
                "foggy" => LevelWeatherType.Foggy,
                "flooded" => LevelWeatherType.Flooded,
                "dustclouds" => LevelWeatherType.DustClouds,
                "eclipsed" => LevelWeatherType.Eclipsed,
                _ => new WeatherNameResolvable(weatherNameResolvable).WeatherType
            };
            AddCombinedWeather(weatherType);
        }

        public static void AddCombinedWeather(LevelWeatherType weather)
        {
            WeatherController.AddWeatherEffect(weather);
        }

        public static bool IsWeatherEffectPresent(string weatherNameResolvable)
        {
            return IsWeatherEffectPresent(new WeatherNameResolvable(weatherNameResolvable).WeatherType);
        }

        public static bool IsWeatherEffectPresent(LevelWeatherType weatherType)
        {
            for (int i = 0; i < WeatherManager.CurrentEffectTypes.Count; i++)
            {
                if (WeatherManager.CurrentEffectTypes[i] == weatherType)
                {
                    return true;
                }
            }
            return false;
        }

        public static void RemoveWeather(string weatherNameResolvable)
        {
            RemoveWeather(new WeatherNameResolvable(weatherNameResolvable).WeatherType);
        }

        public static void RemoveWeather(LevelWeatherType weatherType)
        {
            if (WeatherManager.CurrentEffectTypes.Contains(weatherType))
            {
                WeatherManager.GetWeather(weatherType).Effect.DisableEffect(true);
            }
        }

        public static ImprovedWeatherEffect? GetWeatherEffect(string weatherNameResolvable)
        {
            return GetWeatherEffect(new WeatherNameResolvable(weatherNameResolvable).WeatherType);
        }

        public static ImprovedWeatherEffect? GetWeatherEffect(LevelWeatherType weatherType)
        {
            var weather = WeatherManager.GetWeather(weatherType);
            if (weather == null)
                return null;
            var weatherEffect = WeatherManager.GetWeather(weatherType).Effect;
            return weatherEffect;
        }

        public static void Message(string title, string bottom, bool warning = false)
        {
            HUDManager.Instance.DisplayTip(title, bottom, warning);
        }
    }
}
