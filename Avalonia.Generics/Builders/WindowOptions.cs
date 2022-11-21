using Avalonia.Controls;

namespace Avalonia.Generics.Builders
{
    public record WindowOptions(bool CanResize = true, bool CanMinimize = true, bool CanClose = true, bool ShowInTaskbar = true, SizeToContent SizeToContent = SizeToContent.Manual)
    {
        public static WindowOptions Dialog { get; } = new(CanResize: false, CanMinimize: false, ShowInTaskbar: false, SizeToContent: SizeToContent.WidthAndHeight);
        public static WindowOptions Window { get; } = new();
    }
}
