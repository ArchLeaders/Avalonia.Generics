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
        internal GenericWindow(
            string? title, object? content, object? menu, object? chrome, bool canResize = false, bool canMinimize = true, double minWidth = 0, double minHeight = 0, double maxWidth = double.NaN, double maxHeight = double.NaN, IImage? icon = null)
        {
            InitializeComponent();

            MinWidth = minWidth;
            MinHeight = minHeight;
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;

            // Set title
            Title = title;
            TitleBox.Text = title;

            // Set resize modes
            CanResize = canResize;
            Fullscreen.IsVisible = canResize;
            Minimize.IsVisible = canMinimize;

            // Initialize chrome events
            Minimize.Click += (s, e) => WindowState = WindowState.Minimized;
            Fullscreen.Click += (s, e) => WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            Quit.Click += (s, e) => {
                Result = DialogResult.Cancel;
                Close();
            };

            // Load icon
            DialogIcon.Source = icon ?? App.Icon ?? App.DefaultIcon;
            Icon = new((IBitmap)DialogIcon.Source);

            // Load Menu
            if (menu != null) {
                TitleBox.IsVisible = false;
                RootMenu.Items = MenuFactory.MenuFactory.Generate(menu);
            }

            // Load content
            Content.Content = content;

            Loaded += (s, e) => {
                MinWidth = MinWidth == 0 ? Bounds.Width : MinWidth;
                MinHeight = MinHeight == 0 ? Bounds.Height : MinHeight;
            };
        }

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
