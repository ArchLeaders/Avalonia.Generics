using Material.Icons;

namespace Avalonia.Generics.Factories
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MenuAttribute : Attribute
    {
        public string? Name { get; set; }
        public string? Path { get; set; }
        public string HotKey { get; set; } = string.Empty;
        public MaterialIconKind Icon { get; set; }
        public bool IsSeparator { get; set; } = false;

        public MenuAttribute(string name, string path, string hotkey = "")
        {
            Name = name;
            Path = path;
            HotKey = hotkey;
        }
    }
}
