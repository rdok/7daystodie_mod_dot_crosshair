using UnityEngine;

namespace DotCrosshair.Harmony
{
    public static class DotCrosshair
    {
        private static readonly ILogger Logger = new Logger();

        public static float DotSize { get; set; } = 5f;
        public static byte RedColour { get; set; } = 255;
        public static byte GreenColour { get; set; } = 255;
        public static byte BlueColour { get; set; } = 255;
        public static float AlphaChannel { get; set; } = 0.75f;

        public static float ShadowSize { get; set; } = .1f;
        public static bool ShadowEnabled { get; set; } = true;
        public static float ShadowOffsetX { get; set; } = .5f;
        public static float ShadowOffsetY { get; set; } = 1.5f;

        public static void Draw(EntityPlayerLocal player)
        {
            var center = new Vector2(Screen.width / 2, Screen.height / 2);


            var dotRect = new Rect(center.x - DotSize / 2, center.y - DotSize / 2, DotSize, DotSize);

            if (ShadowEnabled)
            {
                var shadowOffset = new Vector2(ShadowOffsetX, ShadowOffsetY);
                var shadowRect = new Rect(
                    dotRect.x + shadowOffset.x,
                    dotRect.y + shadowOffset.y,
                    DotSize + ShadowSize,
                    DotSize + ShadowSize
                );
                var fullyOpaqueBlack = new Color(0f, 0f, 0f, 0.5f);
                GUI.color = fullyOpaqueBlack;
                GUI.DrawTexture(shadowRect, Texture2D.whiteTexture);
            }

            var alphaChannel = (byte)(AlphaChannel * 255);
            
            var crosshairColor = new Color32(RedColour, GreenColour, BlueColour, alphaChannel);

            GUI.color = crosshairColor;
            GUI.DrawTexture(dotRect, Texture2D.whiteTexture);
        }
    }
}