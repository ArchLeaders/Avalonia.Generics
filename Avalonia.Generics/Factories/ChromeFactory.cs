using Avalonia.Controls;
using Avalonia.Generics.Controls;
using Material.Icons.Avalonia;
using System.Reflection;

namespace Avalonia.Generics.Factories
{
    public class ChromeFactory
    {
        public static void Generate(GenericWindow target, object chromeModel)
        {
            MethodInfo[] methods = chromeModel.GetType().GetMethods().Where(x => x.GetCustomAttributes<ChromeAttribute>().Any()).ToArray();

            foreach (var method in methods) {
                ChromeAttribute att = method.GetCustomAttribute<ChromeAttribute>()!;

                Button button = new() {
                    Content = new MaterialIcon() {
                        Kind = att.Icon,
                        Width = att.IconSize,
                        Height = att.IconSize
                    }
                };

                button.Click += (s, e) => method.Invoke(chromeModel, new object[1] { target });

                if (!string.IsNullOrEmpty(att.Tooltip)) {
                    ToolTip.SetTip(button, att.Tooltip);
                }

                target.ChromeButtons.Children.Add(button);
            }
        }
    }
}
