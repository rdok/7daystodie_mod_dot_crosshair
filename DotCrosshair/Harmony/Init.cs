namespace DotCrosshair.Harmony
{
    public class Init : IModApi
    {
        private static readonly ILogger _logger = new Logger();

        public void InitMod(Mod modInstance)
        {
            _logger.Info("InitMod: Loading Dot Crosshair Mod");
            var harmony = new HarmonyLib.Harmony("uk.co.rdok.7daystodie.mod.dotcrosshair");
            harmony.PatchAll();
            _logger.Info("InitMod: Dot Crosshair Mod loaded successfully.");
        }
    }
}