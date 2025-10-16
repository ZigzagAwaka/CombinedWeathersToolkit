using BepInEx;
using BepInEx.Logging;
using CombinedWeathersToolkit.Toolkit;
using HarmonyLib;

namespace CombinedWeathersToolkit
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency("WeatherTweaks")]
    [BepInDependency("mrov.WeatherRegistry")]
    public class Plugin : BaseUnityPlugin
    {
        const string GUID = "zigzag.combinedweatherstoolkit";
        const string NAME = "Combined_Weathers_Toolkit";
        const string VERSION = "1.0.0";

        public static Plugin instance;
        public static ManualLogSource logger;
        private readonly Harmony harmony = new Harmony(GUID);
        internal static Config config { get; private set; } = null!;

        internal static void DebugLog(object message)
        {
            if (config.DebugLogsEnabled.Value)
                logger.LogWarning(message);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051")]
        void Awake()
        {
            instance = this;
            logger = Logger;
            config = new Config(Config);
            config.SetupCustomConfigs();

            if (config.AllowConfigRegistery.Value)
                ConfigRegistery.LoadAllConfigValues();
            if (config.AllowJsonRegistery.Value)
                JsonRegistery.LoadAllJsonFiles();

            harmony.PatchAll();
            logger.LogInfo($"{NAME} is loaded !");
        }
    }
}
