using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Avalonia.Generics.Demo.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
