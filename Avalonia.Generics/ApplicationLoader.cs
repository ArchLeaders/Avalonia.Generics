using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;

namespace Avalonia.Generics
{
    /// <summary>
    /// <see cref="AvaloniaGenerics"/> initialization class.
    /// </summary>
    public class ApplicationLoader
    {
        public static void Attach(Application app)
        {
            if (app.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow != null) {
                App.View = desktop.MainWindow;
                App.Icon = GetWindowIcon(App.View.Icon);
                App.TopLevel = App.View.GetVisualRoot() as TopLevel
                    ?? throw new Exception($"Could not find visual root on '{App.View.GetType().FullName}'.");
            }
            else {
                throw new NotImplementedException();
            }
        }

        internal static Bitmap? GetWindowIcon(WindowIcon? icon)
        {
            if (icon != null) {
                using MemoryStream stream = new();
                icon.Save(stream);
                stream.Position = 0;
                return new Bitmap(stream);
            }

            return null;
        }
    }
}
