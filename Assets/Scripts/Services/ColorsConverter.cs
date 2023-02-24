using UnityEngine;

namespace DefaultNamespace.Services
{
    public static class ColorsConverter
    {
        public static Color32 ToColor(int HexVal)
        {
            byte R = (byte)((HexVal >> 16) & 0xFF);
            byte G = (byte)((HexVal >> 8) & 0xFF);
            byte B = (byte)((HexVal) & 0xFF);
            return new Color32(R, G, B, 255);
        }
        
        public static string ToHex(Color color)
        {
            Color32 c = color;
            var hex = $"{c.r:X2}{c.g:X2}{c.b:X2}{c.a:X2}";
            return hex;
        }
    }
}