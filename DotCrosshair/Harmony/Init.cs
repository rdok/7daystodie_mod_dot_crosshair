using GearsAPI.Settings;
using GearsAPI.Settings.Global;
using GearsAPI.Settings.World;

namespace DotCrosshair.Harmony
{
    public class Init : IModApi, IGearsModApi
    {
        private static readonly ILogger Logger = new Logger();
        public static IGearsMod gearsMod;

        public void InitMod(IGearsMod modInstance)
        {
            gearsMod = modInstance;
        }

        public void OnGlobalSettingsLoaded(IModGlobalSettings modSettings)
        {
            Logger.Debug("OnGlobalSettingsLoaded");
            var generalTab = modSettings.GetTab("General");
            var dotCategory = generalTab.GetCategory("Dot");

            var dotShape = dotCategory.GetSetting("Shape");
            dotShape.OnSettingChanged += DotShapeChanged;
            DotSizeChanged(dotShape, (dotShape as IGlobalValueSetting)?.CurrentValue);

            var dotSize = dotCategory.GetSetting("Size");
            dotSize.OnSettingChanged += DotSizeChanged;
            DotSizeChanged(dotSize, (dotSize as IGlobalValueSetting)?.CurrentValue);

            var enabledForRangedWeapons = dotCategory.GetSetting("EnabledForRangedWeapons");
            enabledForRangedWeapons.OnSettingChanged += EnabledForRangedWeaponsChanged;
            EnabledForRangedWeaponsChanged(
                enabledForRangedWeapons, (enabledForRangedWeapons as IGlobalValueSetting)?.CurrentValue
            );

            var redColor = dotCategory.GetSetting("Red");
            redColor.OnSettingChanged += DotRedColorChanged;
            DotRedColorChanged(redColor, (redColor as IGlobalValueSetting)?.CurrentValue);

            var greenColor = dotCategory.GetSetting("Green");
            greenColor.OnSettingChanged += DotGreenColorChanged;
            DotGreenColorChanged(greenColor, (greenColor as IGlobalValueSetting)?.CurrentValue);

            var blueColor = dotCategory.GetSetting("Blue");
            blueColor.OnSettingChanged += DotBlueColorChanged;
            DotBlueColorChanged(blueColor, (blueColor as IGlobalValueSetting)?.CurrentValue);

            var alphaChannel = dotCategory.GetSetting("Alpha");
            alphaChannel.OnSettingChanged += DotAlphaChannelChanged;
            DotAlphaChannelChanged(alphaChannel, (alphaChannel as IGlobalValueSetting)?.CurrentValue);

            var squareTab = modSettings.GetTab("Square");

            var shadowEnabled = squareTab.GetCategory("Shadow").GetSetting("Enabled");
            shadowEnabled.OnSettingChanged += ShadowEnabledChanged;
            ShadowEnabledChanged(shadowEnabled, (shadowEnabled as IGlobalValueSetting)?.CurrentValue);

            var shadowSize = squareTab.GetCategory("Shadow").GetSetting("Size");
            shadowSize.OnSettingChanged += ShadowSizeChanged;
            ShadowSizeChanged(shadowSize, (shadowSize as IGlobalValueSetting)?.CurrentValue);

            var shadowOffsetX = squareTab.GetCategory("Shadow").GetSetting("OffsetX");
            shadowOffsetX.OnSettingChanged += ShadowSizeChanged;
            ShadowOffsetXChanged(shadowOffsetX, (shadowOffsetX as IGlobalValueSetting)?.CurrentValue);

            var shadowOffsetY = squareTab.GetCategory("Shadow").GetSetting("OffsetY");
            shadowOffsetY.OnSettingChanged += ShadowSizeChanged;
            ShadowOffsetYChanged(shadowOffsetY, (shadowOffsetY as IGlobalValueSetting)?.CurrentValue);
        }

        private static void DotShapeChanged(IGlobalModSetting setting, string value)
        {
            Logger.Debug($"setting.Name: {setting.Name}. New Value: {value}");
            DotCrosshair.Shape = DotShape.FromString(value);
        }

        public void OnWorldSettingsLoaded(IModWorldSettings worldSettings)
        {
        }

        public void InitMod(Mod modInstance)
        {
            var harmony = new HarmonyLib.Harmony("uk.co.rdok.7daystodie.mod.dotcrosshair");
            harmony.PatchAll();
        }

        private static void DotRedColorChanged(IGlobalModSetting setting, string value)
        {
            Logger.Debug($"setting.Name: {setting.Name}. New Value: {value}");
            byte.TryParse(value, out var colour);
            DotCrosshair.RedColour = colour;
        }

        private static void DotGreenColorChanged(IGlobalModSetting setting, string value)
        {
            Logger.Debug($"setting.Name: {setting.Name}. New Value: {value}");
            byte.TryParse(value, out var colour);
            DotCrosshair.GreenColour = colour;
        }

        private static void DotBlueColorChanged(IGlobalModSetting setting, string value)
        {
            Logger.Debug($"setting.Name: {setting.Name}. New Value: {value}");
            byte.TryParse(value, out var colour);
            DotCrosshair.BlueColour = colour;
        }

        private static void DotAlphaChannelChanged(IGlobalModSetting setting, string value)
        {
            Logger.Debug($"setting.Name: {setting.Name}. New Value: {value}");
            float.TryParse(value, out var alphaChannel);
            DotCrosshair.AlphaChannel = alphaChannel;
        }


        private static void ShadowSizeChanged(IGlobalModSetting setting, string value)
        {
            Logger.Debug($"setting.Name: {setting.Name}. New Value: {value}");
            float.TryParse(value, out var shadowSize);
            DotCrosshair.ShadowSize = shadowSize;
        }

        private static void DotSizeChanged(IGlobalModSetting setting, string value)
        {
            Logger.Debug($"setting.Name: {setting.Name}. New Value: {value}");
            float.TryParse(value, out var dotSize);
            DotCrosshair.DotSize = dotSize;
        }

        private static void ShadowEnabledChanged(IGlobalModSetting setting, string value)
        {
            Logger.Debug($"setting.Name: {setting.Name}. New Value: {value}");
            bool.TryParse(value, out var shadowEnabled);
            DotCrosshair.ShadowEnabled = shadowEnabled;
        }

        private static void EnabledForRangedWeaponsChanged(IGlobalModSetting setting, string value)
        {
            Logger.Debug($"setting.Name: {setting.Name}. New Value: {value}");
            bool.TryParse(value, out var enabledForRangedWeaponsChanged);
            Patch.EnabledForRangedWeaponsSetting = enabledForRangedWeaponsChanged;
        }

        private static void ShadowOffsetXChanged(IGlobalModSetting setting, string value)
        {
            Logger.Debug($"setting.Name: {setting.Name}. New Value: {value}");
            float.TryParse(value, out var shadowOffsetX);
            DotCrosshair.ShadowOffsetX = shadowOffsetX;
        }

        private static void ShadowOffsetYChanged(IGlobalModSetting setting, string value)
        {
            Logger.Debug($"setting.Name: {setting.Name}. New Value: {value}");
            float.TryParse(value, out var shadowOffsetY);
            DotCrosshair.ShadowOffsetY = shadowOffsetY;
        }
    }
}