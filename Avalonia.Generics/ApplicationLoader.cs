using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
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
                App.DefaultIcon = new Bitmap(AvaloniaLocator.Current.GetService<IAssetLoader>()!.Open(new($"avares://Avalonia.Generics/Assets/avalonia.ico")));
                App.View = desktop.MainWindow;

                // Load app icon
                if (App.View.Icon != null) {
                    using MemoryStream stream = new();
                    App.View.Icon.Save(stream);
                    stream.Position = 0;
                    App.Icon = new Bitmap(stream);
                }

                // Construct TopLevel
                App.TopLevel = App.View.GetVisualRoot() as TopLevel ?? throw new Exception($"Could not find visual root on '{App.View.GetType().FullName}'.");
            }
            else {
                throw new NotImplementedException();
            }
        }
    }
}
