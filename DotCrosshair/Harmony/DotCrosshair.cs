using UnityEngine;

namespace DotCrosshair.Harmony
{
    public static class DotCrosshair
    {
        private static readonly ILogger Logger = new Logger();

        public static void Draw(EntityPlayerLocal player)
        {
            Logger.Info("DrawDotCrosshair: Drawing a dot crosshair with shadow.");

            var center = new Vector2(Screen.width / 2, Screen.height / 2);
            Logger.Info($"DrawDotCrosshair: Screen center calculated as {center}.");

            const float dotSize = 6f;

            var shadowOffset = new Vector2(1f, 1f);

            var dotRect = new Rect(center.x - dotSize / 2, center.y - dotSize / 2, dotSize, dotSize);
            var shadowRect = new Rect(dotRect.x + shadowOffset.x, dotRect.y + shadowOffset.y, dotSize, dotSize);

            var fullyOpaqueBlack = new Color(0f, 0f, 0f, 1f);
            GUI.color = fullyOpaqueBlack;
            GUI.DrawTexture(shadowRect, Texture2D.whiteTexture);

            var crosshairAlpha = player.weaponCrossHairAlpha;
            var crosshairColor = new Color(1f, 1f, 1f, crosshairAlpha);
            GUI.color = crosshairColor;
            GUI.DrawTexture(dotRect, Texture2D.whiteTexture);

            GUI.color = Color.white;

            Logger.Info("DrawDotCrosshair: Dot crosshair with shadow drawn.");
        }
    }
}