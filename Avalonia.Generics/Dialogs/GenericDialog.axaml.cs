using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Reflection;

namespace Avalonia.Generics.Dialogs
{
    public partial class GenericDialog : Window
    {
        public MessageBoxResult Result { get; set; }

        public GenericDialog() => throw new NotImplementedException();
        public GenericDialog(string title, object content, bool canResize = false, bool canMinimize = true, double minWidth = 0, double minHeight = 0, IImage? icon = null)
        {
            InitializeComponent();
            DataContext = this;
            MinWidth = minWidth;
            MinHeight = minHeight;

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
                Result = MessageBoxResult.Cancel;
                Close();
            };

            // Load icon
            DialogIcon.Source = icon ?? App.Icon ?? App.DefaultIcon;
            Icon = new((IBitmap)DialogIcon.Source);

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

        public void MinimizeEvent()
        {
            WindowState = WindowState.Minimized;
        }

        public void FullscreenEvent()
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        public void QuitEvent()
        {
            Result = MessageBoxResult.Cancel;
            Close();
        }
    }
}
