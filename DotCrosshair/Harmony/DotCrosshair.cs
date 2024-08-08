using UnityEngine;

namespace DotCrosshair.Harmony
{
    public static class DotCrosshair
    {
        private static readonly ILogger Logger = new Logger();

        public static void Draw(EntityPlayerLocal player)
        {
            Logger.Info("DotCrosshair: Drawing a dot crosshair with shadow.");

            var center = new Vector2(Screen.width / 2, Screen.height / 2);
            Logger.Info($"DotCrosshair: Screen center calculated as {center}.");

            const float dotSize = 5f;
            var shadowOffset = new Vector2(0.5f, 1.5f);
            const float shadowSize = .1f;

            var dotRect = new Rect(center.x - dotSize / 2, center.y - dotSize / 2, dotSize, dotSize);
            var shadowRect = new Rect(
                dotRect.x + shadowOffset.x,
                dotRect.y + shadowOffset.y,
                dotSize + shadowSize,
                dotSize + shadowSize
            );

            var fullyOpaqueBlack = new Color(0f, 0f, 0f, 0.5f); 
            GUI.color = fullyOpaqueBlack;
            GUI.DrawTexture(shadowRect, Texture2D.whiteTexture);

            var crosshairAlpha = player.weaponCrossHairAlpha * 0.75f; 
            var crosshairColor = new Color(1f, 1f, 1f, crosshairAlpha);
            GUI.color = crosshairColor;
            GUI.DrawTexture(dotRect, Texture2D.whiteTexture);

            Logger.Info("DotCrosshair: Dot crosshair with shadow drawn.");
        }
    }
}