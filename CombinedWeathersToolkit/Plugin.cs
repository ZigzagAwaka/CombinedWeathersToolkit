using BepInEx;
using BepInEx.Logging;
using CombinedWeathersToolkit.Toolkit;

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
        internal static Config config { get; private set; } = null!;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051")]
        void Awake()
        {
            instance = this;
            logger = Logger;
            config = new Config(Config);
            config.SetupCustomConfigs();

            if (config.RegisterPredefinedCombinedWeathers.Value)
                PredefinedWeathers.Setup();
            if (config.AllowConfigRegistery.Value)
                ConfigRegistery.Setup();
            if (config.AllowJsonRegistery.Value)
                JsonRegistery.Setup();

            logger.LogInfo($"{NAME} is loaded !");
        }
    }
}
