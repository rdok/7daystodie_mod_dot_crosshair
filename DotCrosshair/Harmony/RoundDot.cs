using UnityEngine;

namespace DotCrosshair.Harmony
{
    public static class RoundDot
    {
        private static Texture2D _cachedCircleTexture;
        private static int _cachedDotSize;

        private static Texture2D GenerateCircleTexture(int dotDiameter)
        {
            if (_cachedCircleTexture != null && _cachedDotSize == dotDiameter)
            {
                return _cachedCircleTexture;
            }

            _cachedDotSize = dotDiameter;

            var circleTexture = new Texture2D(dotDiameter, dotDiameter, TextureFormat.ARGB32, false);
            var fillColor = new Color32(255, 255, 255, 255);
            var transparentColor = new Color32(255, 255, 255, 0);

            var circleRadius = (dotDiameter - 1) / 2f;
            var circleCenter = new Vector2(circleRadius, circleRadius);

            for (var pixelX = 0; pixelX < dotDiameter; pixelX++)
            {
                for (var pixelY = 0; pixelY < dotDiameter; pixelY++)
                {
                    var distanceX = pixelX - circleCenter.x;
                    var distanceY = pixelY - circleCenter.y;
                    var distanceFromCenter = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);

                    if (distanceFromCenter < circleRadius - 0.5f)
                    {
                        circleTexture.SetPixel(pixelX, pixelY, fillColor);
                    }
                    else if (distanceFromCenter < circleRadius + 0.5f)
                    {
                        var edgeAlpha = Mathf.Clamp01(1f - (distanceFromCenter - circleRadius + 0.5f));
                        var edgeColor = new Color(1f, 1f, 1f, edgeAlpha);
                        circleTexture.SetPixel(pixelX, pixelY, edgeColor);
                    }
                    else
                    {
                        circleTexture.SetPixel(pixelX, pixelY, transparentColor);
                    }
                }
            }

            circleTexture.Apply();
            _cachedCircleTexture = circleTexture;

            return _cachedCircleTexture;
        }

        public static void Render(Vector2 screenPosition, float circleDiameter, Color32 circleColor)
        {
            var circleTexture = GenerateCircleTexture((int)circleDiameter);
            var circleRect = new Rect(screenPosition.x - circleDiameter / 2, screenPosition.y - circleDiameter / 2,
                circleDiameter, circleDiameter);

            GUI.color = circleColor;
            GUI.DrawTexture(circleRect, circleTexture);
        }
    }
}