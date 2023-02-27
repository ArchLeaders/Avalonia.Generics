namespace Avalonia.Generics.Factories
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ChromeAttribute : Attribute
    {
        public ChromeAttribute(string icon, string? tooltip = null)
        {
            Icon = icon;
            Tooltip = tooltip;
        }

        public string Icon { get; set; }
        public string? Tooltip { get; set; }
        public double IconSize { get; set; } = 20;
    }
}
