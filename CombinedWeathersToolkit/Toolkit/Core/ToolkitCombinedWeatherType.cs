using WeatherRegistry;
using WeatherTweaks.Definitions;

namespace CombinedWeathersToolkit.Toolkit.Core
{
    internal class ToolkitCombinedWeatherType : CombinedWeatherType
    {
        internal ToolkitWeather ToolkitWeather;

        internal ToolkitCombinedWeatherType(ToolkitWeather toolkit) : base(toolkit.Name, toolkit.Weathers, toolkit.WeightModifier ?? 0.2f)
        {
            ToolkitWeather = toolkit;
            ToolkitHelper.SetColor(toolkit, this);
        }

        public override string ConfigCategory
        {
            get
            {
                return ToolkitHelper.GetConfigCategory(ToolkitWeather);
            }
        }

        public override void Init()
        {
            ToolkitHelper.SetConfig(ToolkitWeather, this);
            if (ToolkitWeather.Weight.HasValue)
            {
                Config.DefaultWeight = new IntegerConfigHandler(ToolkitWeather.Weight.Value);
                // Manually invoke base.Init() to avoid overriding default weight
                var methodPointer = typeof(WeatherTweaksWeather).GetMethod("Init").MethodHandle.GetFunctionPointer();
                ((System.Action)System.Activator.CreateInstance(typeof(System.Action), this, methodPointer)).Invoke();
            }
            else
                base.Init();
        }

        public override (float valueMultiplier, float amountMultiplier) GetDefaultMultiplierData()
        {
            if (!ToolkitWeather.ScrapValueMultiplier.HasValue && !ToolkitWeather.ScrapAmountMultiplier.HasValue)
            {
                var defaultMultipliers = base.GetDefaultMultiplierData();
                if (ToolkitHelper.ShouldResetAmountData(ToolkitWeather))
                    defaultMultipliers.amountMultiplier = 1f;
                return defaultMultipliers;
            }
            return ToolkitHelper.GetMultipliers(ToolkitWeather);
        }
    }
}
