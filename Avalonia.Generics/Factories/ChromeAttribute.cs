using Material.Icons;

namespace Avalonia.Generics.Factories
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ChromeAttribute : Attribute
    {
        public ChromeAttribute(MaterialIconKind icon, string? tooltip = null)
        {
            Icon = icon;
            Tooltip = tooltip;
        }

        public MaterialIconKind Icon { get; set; }
        public string? Tooltip { get; set; }
        public double IconSize { get; set; } = 20;
    }
}
