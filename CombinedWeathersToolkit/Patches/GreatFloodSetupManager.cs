using HarmonyLib;
using System.Linq;
using WeatherRegistry;
using WeatherRegistry.Definitions;

namespace CombinedWeathersToolkit.Patches
{
    [HarmonyPatch(typeof(TimeOfDay))]
    internal class GreatFloodWaterRisingManager
    {
        public static RandomWeatherWithVariables? floodEffect;
        public static bool searchForEffect = true;


        /// <summary>
        /// Manage the water level of different tides during the Great Flood weather
        /// </summary>
        [HarmonyPostfix]
        [HarmonyPatch("MoveTimeOfDay")]
        public static void OverrideGreatFloodWaterLevel()
        {
            if (WeatherManager.GetCurrentLevelWeather().Name == "The Great Flood" && GreatFloodSetupManager.originalMinWaterLevel.HasValue && GreatFloodSetupManager.originalMaxWaterLevel.HasValue)
            {
                if (searchForEffect)
                {
                    floodEffect = StartOfRound.Instance.currentLevel.randomWeathers.FirstOrDefault(x => x.weatherType == LevelWeatherType.Flooded);
                    searchForEffect = false;
                }
                else if (floodEffect != null)
                {
                    if ((TimeOfDay.Instance.currentDayTime >= 360 && TimeOfDay.Instance.currentDayTime < 450) || (TimeOfDay.Instance.currentDayTime >= 690 && TimeOfDay.Instance.currentDayTime < 780))  // 12:00-13:30 and 17:30-19:00
                    {
                        floodEffect.weatherVariable = floodEffect.weatherVariable2 = (int)StartOfRound.Instance.middleOfShipNode.transform.position.y + 7;
                    }
                    else if ((TimeOfDay.Instance.currentDayTime >= 450 && TimeOfDay.Instance.currentDayTime < 690) || (TimeOfDay.Instance.currentDayTime >= 780 && TimeOfDay.Instance.currentDayTime < 982))  // 13:30-17:30 and 19:00-22:22
                    {
                        floodEffect.weatherVariable = GreatFloodSetupManager.originalMinWaterLevel.Value;
                        floodEffect.weatherVariable2 = GreatFloodSetupManager.originalMaxWaterLevel.Value;
                    }
                    else if (TimeOfDay.Instance.currentDayTime >= 982)  // 22:22
                    {
                        floodEffect.weatherVariable = floodEffect.weatherVariable2 = (int)StartOfRound.Instance.middleOfShipNode.transform.position.y;
                    }
                }
            }
        }
    }


    [HarmonyPatch(typeof(StartOfRound))]
    internal class GreatFloodSetupManager
    {
        internal static bool originalEffectExist = true;
        internal static int? originalMinWaterLevel = null;
        internal static int? originalMaxWaterLevel = null;


        /// <summary>
        /// Save the original water level of the Great Flood weather at the start of the round.
        /// If the effect doesn't exist (the level does not support Flooded), create it with a calculated water level
        /// </summary>
        [HarmonyPostfix]
        [HarmonyPatch("OnShipLandedMiscEvents")]
        public static void SaveGreatFloodOriginalWaterLevel()
        {
            if (WeatherManager.GetCurrentLevelWeather().Name == "The Great Flood")
            {
                int minWaterLevel = (int)RoundManager.Instance.outsideAINodes.Min(node => node.transform.position.y);
                int maxWaterLevel = minWaterLevel + 3;
                RandomWeatherWithVariables floodEffect = StartOfRound.Instance.currentLevel.randomWeathers.FirstOrDefault(x => x.weatherType == LevelWeatherType.Flooded);

                if (floodEffect == null)
                {
                    StartOfRound.Instance.currentLevel.randomWeathers = StartOfRound.Instance.currentLevel.randomWeathers.Append(new ImprovedRandomWeatherWithVariables()
                    {
                        weatherType = LevelWeatherType.Flooded,
                        weatherVariable = minWaterLevel,
                        weatherVariable2 = maxWaterLevel
                    }).ToArray();
                    originalEffectExist = false;
                    originalMinWaterLevel = minWaterLevel;
                    originalMaxWaterLevel = maxWaterLevel;
                }
                else
                {
                    originalMinWaterLevel = floodEffect.weatherVariable;
                    originalMaxWaterLevel = floodEffect.weatherVariable2;
                }
            }
        }


        /// <summary>
        /// Reset the water level of the flood effect at the end of the round
        /// </summary>
        [HarmonyPrefix]
        [HarmonyPatch("SetShipReadyToLand")]
        public static void ResetGreatFloodWaterLevel()
        {
            if (originalMinWaterLevel != null && originalMaxWaterLevel != null)
            {
                if (!originalEffectExist)
                {
                    StartOfRound.Instance.currentLevel.randomWeathers = StartOfRound.Instance.currentLevel.randomWeathers.Where(x => x.weatherType != LevelWeatherType.Flooded).ToArray();
                }
                else
                {
                    RandomWeatherWithVariables floodEffect = StartOfRound.Instance.currentLevel.randomWeathers.FirstOrDefault(x => x.weatherType == LevelWeatherType.Flooded);
                    floodEffect.weatherVariable = originalMinWaterLevel.Value;
                    floodEffect.weatherVariable2 = originalMaxWaterLevel.Value;
                }
                originalMinWaterLevel = null;
                originalMaxWaterLevel = null;
                originalEffectExist = true;
                GreatFloodWaterRisingManager.searchForEffect = true;
                GreatFloodWaterRisingManager.floodEffect = null;
            }
        }
    }
}