using CombinedWeathersToolkit.Utils;
using HarmonyLib;
using System.Collections.Generic;
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

        public static readonly List<(string message, bool hasHappened)> tidesWarnings = new List<(string, bool)>()
        {
            ("High tide approaching! Water level will rise at its peak very soon. Seek higher ground immediately!", false),
            ("Low tide is coming! Water level will start to drop, take this chance to meet the quota!", false),
            ("High tide detected! The flood water level is unstable. Please take refuge in a higher location!", false),
            ("Low tide is coming! The flood water level is now droping. Stay safe!", false),
            ("High tide is expected! Water level is starting to rise again, be careful!", false)
        };


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
                    if (TimeOfDay.Instance.currentDayTime >= 350 && !tidesWarnings[0].hasHappened)
                    {
                        Effects.MessageComputer(tidesWarnings[0].message);
                        tidesWarnings[0] = (tidesWarnings[0].message, true);
                    }
                    else if (TimeOfDay.Instance.currentDayTime >= 440 && !tidesWarnings[1].hasHappened)
                    {
                        Effects.MessageComputer(tidesWarnings[1].message);
                        tidesWarnings[1] = (tidesWarnings[1].message, true);
                    }
                    else if (TimeOfDay.Instance.currentDayTime >= 680 && !tidesWarnings[2].hasHappened)
                    {
                        Effects.MessageComputer(tidesWarnings[2].message);
                        tidesWarnings[2] = (tidesWarnings[2].message, true);
                    }
                    else if (TimeOfDay.Instance.currentDayTime >= 770 && !tidesWarnings[3].hasHappened)
                    {
                        Effects.MessageComputer(tidesWarnings[3].message);
                        tidesWarnings[3] = (tidesWarnings[3].message, true);
                    }
                    else if (TimeOfDay.Instance.currentDayTime >= 962 && !tidesWarnings[4].hasHappened)
                    {
                        Effects.MessageComputer(tidesWarnings[4].message);
                        tidesWarnings[4] = (tidesWarnings[4].message, true);
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

                for (int i = 0; i < GreatFloodWaterRisingManager.tidesWarnings.Count; i++)
                {
                    GreatFloodWaterRisingManager.tidesWarnings[i] = (GreatFloodWaterRisingManager.tidesWarnings[i].message, false);
                }
            }
        }
    }
}