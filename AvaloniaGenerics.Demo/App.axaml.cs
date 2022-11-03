using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaGenerics.Demo.ViewModels;
using AvaloniaGenerics.Demo.Views;
using AvaloniaGenerics.Dialogs;

namespace AvaloniaGenerics.Demo
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
                MainWindow view = new() {
                    DataContext = new MainWindowViewModel(),
                };
                desktop.MainWindow = view;
            }

            ApplicationLoader.Attach(this);
            base.OnFrameworkInitializationCompleted();
        }
    }
}
