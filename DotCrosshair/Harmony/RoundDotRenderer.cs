using UnityEngine;

namespace DotCrosshair.Harmony
{
    public class RoundDotRenderer
    {
        public static Texture2D CreateSmoothCircleTexture(int size)
        {
            Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
            Color32 white = new Color32(255, 255, 255, 255);
            Color32 clear = new Color32(255, 255, 255, 0);

            float radius = (size - 1) / 2f; // Adjusted the radius to ensure complete coverage
            Vector2 center = new Vector2(radius, radius);

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    float dx = x - center.x;
                    float dy = y - center.y;
                    float dist = Mathf.Sqrt(dx * dx + dy * dy);

                    if (dist < radius - 0.5f) // Inside the circle, solid
                    {
                        texture.SetPixel(x, y, white);
                    }
                    else if (dist < radius + 0.5f) // Edge pixels, apply smooth anti-aliasing
                    {
                        float alpha = Mathf.Clamp01(1f - (dist - radius + 0.5f));
                        Color pixelColor = new Color(1f, 1f, 1f, alpha);
                        texture.SetPixel(x, y, pixelColor);
                    }
                    else // Outside the circle
                    {
                        texture.SetPixel(x, y, clear);
                    }
                }
            }

            texture.Apply();
            return texture;
        }

        public static void RenderCircle(Vector2 center, float dotSize, Color32 color)
        {
            Texture2D circleTexture = CreateSmoothCircleTexture((int)dotSize);
            var dotRect = new Rect(center.x - dotSize / 2, center.y - dotSize / 2, dotSize, dotSize);

            GUI.color = color;
            GUI.DrawTexture(dotRect, circleTexture);
        }
    }
}