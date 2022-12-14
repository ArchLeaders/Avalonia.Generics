using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Generics.Demo.ViewModels;
using Avalonia.Generics.Demo.Views;
using Avalonia.Generics.Dialogs;
using Avalonia.Generics.Builders;

namespace Avalonia.Generics.Demo
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
                desktop.MainWindow = WindowBuilder.Initialize(view).WithWindowColors("SystemChromeLowColor", "SystemChromeMediumColor").Build();
            }

            ApplicationLoader.Attach(this);
            base.OnFrameworkInitializationCompleted();
        }
    }
}
