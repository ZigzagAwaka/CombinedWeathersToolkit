using CombinedWeathersToolkit.Utils;
using HarmonyLib;
using System.Linq;

namespace CombinedWeathersToolkit.Patches
{
    [HarmonyPatch(typeof(HUDManager))]
    internal class DebugCommandsManager
    {
        [HarmonyPostfix]
        [HarmonyPatch("AddChatMessage")]
        public static void AddChatMessageDebugCommand(string chatMessage)
        {
            if (!Plugin.config.DebugCommandsEnabled.Value || StartOfRound.Instance == null || StartOfRound.Instance.inShipPhase ||
                !StartOfRound.Instance.shipHasLanded || StartOfRound.Instance.shipIsLeaving)
            {
                return;
            }
            if (chatMessage.StartsWith("/cwt"))
            {
                var command = chatMessage.Split(' ');
                if (command.Length <= 1)
                    return;
                ExecuteCommand(command);
            }
        }


        private static void ExecuteCommand(string[] command)
        {
            switch (command[1])
            {
                case "clear":
                    Effects.ChangeWeather(LevelWeatherType.None);
                    Effects.Message("Debug CWT", "Removed all weathers from current level");
                    break;
                case "list":
                    Effects.Message("Debug CWT", "Current weathers: " + Effects.GetListOfCurrentWeathers());
                    break;
                case "algo":
                    string weatherName = command.Length > 2 ? string.Join(" ", command.Skip(2)) : "";
                    Effects.ForceSetWeatherAlgorithm(weatherName);
                    if (weatherName != "")
                        Effects.Message("Debug CWT", "Forced weather algorithm to " + weatherName);
                    else
                        Effects.Message("Debug CWT", "Reseted weather algorithm to normal behavior");
                    break;
                default:
                    Effects.AddCombinedWeather(command[1]);
                    Effects.Message("Debug CWT", "Spawned " + command[1] + " combined weather");
                    break;
            }
        }
    }
}
