using CombinedWeathersToolkit.Utils;
using HarmonyLib;

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
                switch (command[1])
                {
                    case "clear":
                        Effects.ChangeWeather(LevelWeatherType.None);
                        Effects.Message("Debug CWT", "Removed all weathers from current level");
                        break;
                    default:
                        Effects.AddCombinedWeather(command[1]);
                        Effects.Message("Debug CWT", "Spawned " + command[1] + " combined weather");
                        break;
                }
            }
        }
    }
}
