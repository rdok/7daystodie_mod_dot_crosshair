using System;
using HarmonyLib;
using UnityEngine;

namespace DotCrosshair.Harmony
{
    public class DotCrosshair
    {
        private static ILogger _logger = new Logger();

        public static void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public static void ApplyPatch()
        {
            _logger.Info("ApplyPatch: Starting patch application.");
            try
            {
                var harmony = new HarmonyLib.Harmony("com.dotcrosshair.patch");
                harmony.PatchAll();
                _logger.Info("ApplyPatch: Harmony patches applied successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error($"ApplyPatch: Error applying Harmony patches: {ex}");
            }
        }

        // Patch the OnHUD method of EntityPlayerLocal to replace crosshair logic
        [HarmonyPatch(typeof(EntityPlayerLocal), nameof(EntityPlayerLocal.OnHUD))]
        public static class CrosshairPatch
        {
            public static bool Prefix(EntityPlayerLocal __instance)
            {
                _logger.Info("CrosshairPatch: Prefix called on EntityPlayerLocal.OnHUD.");

                // Suppress original crosshair drawing
                DrawDotCrosshair();

                // Return false to skip original OnHUD execution for crosshair rendering
                return false;
            }
        }

        private static void DrawDotCrosshair()
        {
            _logger.Info("DrawDotCrosshair: Drawing a dot crosshair.");

            // Get the center of the screen
            Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
            _logger.Info($"DrawDotCrosshair: Screen center calculated as {center}.");

            // Define the size of the dot
            float dotSize = 6f;

            // Create a rectangle for the dot
            Rect dotRect = new Rect(center.x - dotSize / 2, center.y - dotSize / 2, dotSize, dotSize);
            _logger.Info($"DrawDotCrosshair: Dot rectangle created at {dotRect}.");

            // Get the current GUI color to use the default color
            Color originalColor = GUI.color;
            _logger.Info($"DrawDotCrosshair: Using default GUI color {originalColor}.");

            // Draw the dot using the current GUI color
            GUI.DrawTexture(dotRect, Texture2D.whiteTexture);
            _logger.Info("DrawDotCrosshair: Dot crosshair drawn.");
        }

        public class Init : IModApi
        {
            public void InitMod(Mod modInstance)
            {
                _logger.Info("InitMod: Loading Dot Crosshair Mod");
                ApplyPatch();
                _logger.Info("InitMod: Dot Crosshair Mod loaded successfully.");
            }
        }
    }
}