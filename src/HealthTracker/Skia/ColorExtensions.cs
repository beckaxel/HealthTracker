using SkiaSharp;
using Xamarin.Forms;

namespace HealthTracker.Skia
{
    public static class ColorConversionExtensions
    {
        public static Color ToColor(this SKColor color)
        {
            return Color.FromHex(color.ToString());
        }

        public static SKColor ToSKColor(this Color color)
        {
            return SKColor.Parse(color.ToHex());
        }
    }
}
