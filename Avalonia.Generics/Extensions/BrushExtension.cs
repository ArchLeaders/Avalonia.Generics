using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;

namespace Avalonia.Generics.Extensions
{
    public static class BrushExtension
    {
        public static Brush GetBrush(this string colour)
        {
            return new SolidColorBrush(Convert.ToUInt32(colour.Remove(0, 1).PadLeft(8, 'F'), 16));
        }

        public static DynamicResourceExtension GetResource(this string name)
        {
            return new DynamicResourceExtension(name);
        }
    }
}
