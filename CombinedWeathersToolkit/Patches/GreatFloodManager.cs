using HarmonyLib;
using System.Linq;
using WeatherRegistry;

namespace CombinedWeathersToolkit.Patches
{
    [HarmonyPatch(typeof(TimeOfDay))]
    internal class GreatFloodWaterRisingManager
    {
        public static RandomWeatherWithVariables? floodEffect;
        public static bool searchForEffect = true;

        [HarmonyPostfix]
        [HarmonyPatch("MoveTimeOfDay")]
        public static void RiseWaterGreatFlood()
        {
            if (WeatherManager.GetCurrentLevelWeather().Name == "The Great Flood" && GreatFloodManager.originalMinWaterLevel.HasValue && GreatFloodManager.originalMaxWaterLevel.HasValue)
            {
                if (searchForEffect)
                {
                    floodEffect = StartOfRound.Instance.currentLevel.randomWeathers.FirstOrDefault(x => x.weatherType == LevelWeatherType.Flooded);
                    searchForEffect = false;
                }
                else if (floodEffect != null)
                {
                    if ((TimeOfDay.Instance.currentDayTime >= 360 && TimeOfDay.Instance.currentDayTime < 450) || (TimeOfDay.Instance.currentDayTime >= 720 && TimeOfDay.Instance.currentDayTime < 780))  // 12:00-13:30 and 18:00-19:00
                    {
                        floodEffect.weatherVariable = floodEffect.weatherVariable2 = (int)StartOfRound.Instance.middleOfShipNode.transform.position.y + 7;
                    }
                    else if ((TimeOfDay.Instance.currentDayTime >= 450 && TimeOfDay.Instance.currentDayTime < 720) || (TimeOfDay.Instance.currentDayTime >= 780 && TimeOfDay.Instance.currentDayTime < 982))  // 13:30-18:00 and 19:00-22:22
                    {
                        floodEffect.weatherVariable = GreatFloodManager.originalMinWaterLevel.Value;
                        floodEffect.weatherVariable2 = GreatFloodManager.originalMaxWaterLevel.Value;
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
    internal class GreatFloodManager
    {
        internal static bool originalEffectExist = true;
        internal static int? originalMinWaterLevel = null;
        internal static int? originalMaxWaterLevel = null;


        [HarmonyPostfix]
        [HarmonyPatch("OnShipLandedMiscEvents")]
        public static void OverrideGreatFloodWaterLevel()
        {
            if (WeatherManager.GetCurrentLevelWeather().Name == "The Great Flood")
            {
                int xx = ((int)RoundManager.Instance.outsideAINodes.Min(node => node.transform.position.y)) - 4;
                int minWaterLevel = ((int)RoundManager.Instance.outsideAINodes.Average(node => node.transform.position.y)) - 4;
                int maxWaterLevel = (int)StartOfRound.Instance.shipDoorNode.transform.position.y + 7;
                RandomWeatherWithVariables floodEffect = StartOfRound.Instance.currentLevel.randomWeathers.FirstOrDefault(x => x.weatherType == LevelWeatherType.Flooded);

                if (floodEffect == null)
                {
                    StartOfRound.Instance.currentLevel.randomWeathers.Append(new RandomWeatherWithVariables()
                    {
                        weatherType = LevelWeatherType.Flooded,
                        weatherVariable = minWaterLevel,
                        weatherVariable2 = maxWaterLevel
                    });
                    originalEffectExist = false;
                }
                else
                {
                    originalMinWaterLevel = floodEffect.weatherVariable;
                    originalMaxWaterLevel = floodEffect.weatherVariable2;
                    //floodEffect.weatherVariable = xx;
                    //floodEffect.weatherVariable2 = floodEffect.weatherVariable;
                }

            }

        }


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
