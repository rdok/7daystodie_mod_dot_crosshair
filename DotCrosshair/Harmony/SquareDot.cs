using UnityEngine;

namespace DotCrosshair.Harmony
{
    public static class SquareDot
    {
        public static void Render(
            Vector2 screenPosition,
            float dotSize,
            Color32 color,
            bool shadowEnabled,
            float shadowSize,
            float shadowOffsetX,
            float shadowOffsetY
        )
        {
            var squareRect = new Rect(screenPosition.x - dotSize / 2, screenPosition.y - dotSize / 2, dotSize, dotSize);

            if (shadowEnabled)
            {
                var shadowPosition = new Vector2(shadowOffsetX, shadowOffsetY);
                var shadowRect = new Rect(
                    squareRect.x + shadowPosition.x,
                    squareRect.y + shadowPosition.y,
                    dotSize + shadowSize,
                    dotSize + shadowSize
                );
                var shadowColor = new Color(0f, 0f, 0f, 0.5f);
                GUI.color = shadowColor;
                GUI.DrawTexture(shadowRect, Texture2D.whiteTexture);
            }

            GUI.color = color;
            GUI.DrawTexture(squareRect, Texture2D.whiteTexture);
        }
    }
}