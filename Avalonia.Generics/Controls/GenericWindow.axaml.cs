using Avalonia;
using Avalonia.Controls;
using Avalonia.Generics.Dialogs;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.MenuFactory;
using Avalonia.Platform;
using System.Reflection;

namespace Avalonia.Generics.Controls
{
    public partial class GenericWindow : Window
    {
        public DialogResult Result { get; set; }

        public GenericWindow() => InitializeComponent();

        public async Task ShowDialog() => await ShowDialog(App.View);

        protected override void HandleWindowStateChanged(WindowState state)
        {
            if (state == WindowState.Maximized) {
                Padding = new Thickness(7);
                ExtendClientAreaTitleBarHeightHint = 44;
                ICON_Fullscreen.IsVisible = !(ICON_Restore.IsVisible = true);
            }
            else {
                Padding = new Thickness(0);
                ExtendClientAreaTitleBarHeightHint = 30;
                ICON_Fullscreen.IsVisible = !(ICON_Restore.IsVisible = false);
            }

            base.HandleWindowStateChanged(state);
        }
    }
}
