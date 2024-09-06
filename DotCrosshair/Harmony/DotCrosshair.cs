using UnityEngine;

namespace DotCrosshair.Harmony
{
    public static class DotCrosshair
    {
        public static DotShape Shape { get; set; } = ModSettings.GetDotShape("General", "Dot", "Shape");
        public static float DotSize { get; set; } = ModSettings.GetFloat("General", "Dot", "Size");
        public static byte RedColour { get; set; } = ModSettings.GetByte("General", "Dot", "Red");
        public static byte GreenColour { get; set; } = ModSettings.GetByte("General", "Dot", "Green");
        public static byte BlueColour { get; set; } = ModSettings.GetByte("General", "Dot", "Blue");
        public static float AlphaChannel { get; set; } = ModSettings.GetFloat("General", "Dot", "Alpha");

        public static float ShadowSize { get; set; } = ModSettings.GetFloat("Square", "Shadow", "Size");
        public static bool ShadowEnabled { get; set; } = ModSettings.GetBool("Square", "Shadow", "Enabled");
        public static float ShadowOffsetX { get; set; } = ModSettings.GetFloat("Square", "Shadow", "OffsetX");
        public static float ShadowOffsetY { get; set; } = ModSettings.GetFloat("Square", "Shadow", "OffsetY");



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