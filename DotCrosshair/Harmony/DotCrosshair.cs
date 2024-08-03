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

        private static void ApplyPatch()
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

        private static void DrawDotCrosshair()
        {
            _logger.Info("DrawDotCrosshair: Drawing a dot crosshair.");

            var center = new Vector2(Screen.width / 2, Screen.height / 2);
            _logger.Info($"DrawDotCrosshair: Screen center calculated as {center}.");

            const float dotSize = 5f;

            var dotRect = new Rect(center.x - dotSize / 2, center.y - dotSize / 2, dotSize, dotSize);
            _logger.Info($"DrawDotCrosshair: Dot rectangle created at {dotRect}.");

            GUI.color = Color.red;
            GUI.DrawTexture(dotRect, Texture2D.whiteTexture);
            _logger.Info("DrawDotCrosshair: Dot crosshair drawn.");
        }

        [HarmonyPatch(typeof(EntityPlayerLocal), nameof(EntityPlayerLocal.OnHUD))]
        public static class CrosshairPatch
        {
            public static void Postfix(EntityPlayerLocal __instance)
            {
                _logger.Info("CrosshairPatch: Postfix called on EntityPlayerLocal.OnHUD.");

                DrawDotCrosshair();
            }
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