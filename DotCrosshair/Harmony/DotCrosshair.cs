using UnityEngine;

namespace DotCrosshair.Harmony
{
    public static class DotCrosshair
    {
        private static readonly ILogger Logger = new Logger();

        public static DotShape Shape { get; set; } = DotShape.Round;

        public static float DotSize { get; set; } = 8f;
        public static byte RedColour { get; set; } = 255;
        public static byte GreenColour { get; set; } = 255;
        public static byte BlueColour { get; set; } = 255;
        public static float AlphaChannel { get; set; } = 0.75f;

        public static float ShadowSize { get; set; } = .1f;
        public static bool ShadowEnabled { get; set; } = true;
        public static float ShadowOffsetX { get; set; } = .5f;
        public static float ShadowOffsetY { get; set; } = 1.5f;

        public static void Draw(EntityPlayerLocal entityPlayerLocal)
        {
            var center = entityPlayerLocal.GetCrosshairPosition2D(); 
            center.y = Screen.height - center.y;
            var alphaChannel = (byte)(AlphaChannel * 255);
            var crosshairColor = new Color32(RedColour, GreenColour, BlueColour, alphaChannel);

            if (Shape.IsSquare())
            {
                SquareDot.Render(
                    center, DotSize, crosshairColor, ShadowEnabled, ShadowSize, ShadowOffsetX, ShadowOffsetY
                );
                return;
            }

            RoundDot.Render(center, DotSize, crosshairColor);
        }
    }
}