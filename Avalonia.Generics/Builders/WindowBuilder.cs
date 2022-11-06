using Avalonia.Controls;
using Avalonia.Generics.Controls;
using Avalonia.Generics.Dialogs;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Material.Icons;

namespace Avalonia.Generics.Builders
{
    public class WindowBuilder
    {
        private readonly GenericWindow Window = new();

        /// <summary>
        /// Adds a <see cref="string"/> Title to the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="title"></param>
        public WindowBuilder WithTitle(string title)
        {
            // Properties[nameof(title)] = title;
            Window.Title = title;
            Window.TitleBox.Text = title;
            return this;
        }

        /// <summary>
        /// Adds an <see cref="IImage"/> icon to the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="icon"></param>
        public WindowBuilder WithIcon(IImage icon)
        {
            Window.DialogIcon.Source = icon;
            Window.Icon = new((IBitmap)Window.DialogIcon.Source);
            return this;
        }

        /// <summary>
        /// Adds a <see cref="MaterialIconKind"/> icon to the current <see cref="WindowBuilder"/>
        /// <para><i>Note: <see cref="MaterialIconKind"/> icons don't support setting the Taskbar Icon, which will be set the parent app icon when possible.</i></para>
        /// </summary>
        /// <param name="icon"></param>
        public WindowBuilder WithIcon(MaterialIconKind icon)
        {
            Window.DialogIcon.IsVisible = false;
            Window.MaterialIcon.IsVisible = true;
            Window.MaterialIcon.Kind = icon;
            return this;
        }

        /// <summary>
        /// Sets the current <see cref="WindowBuilder"/> contents
        /// </summary>
        /// <param name="content"></param>
        public WindowBuilder WithContent(object content)
        {
            Window.Content.Content = content;
            return this;
        }

        /// <summary>
        /// Adds a Menu object to the current <see cref="WindowBuilder"/> to be parsed by the <see cref="MenuFactory.MenuFactory"/> default implementation
        /// </summary>
        /// <param name="menuFactory">A class instance defining the <see cref="MenuItem"/>s as <see cref="MenuFactory.Attributes.MenuAttribute"/> attributed methods</param>
        public WindowBuilder WithMenu(object menuFactory)
        {
            Window.TitleBox.IsVisible = false;
            Window.RootMenu.Items = MenuFactory.MenuFactory.Generate(menuFactory);
            return this;
        }

        /// <summary>
        /// <!-- Adds a Menu object to the current <see cref="WindowBuilder"/> to be parsed by the <see cref="MenuFactory.MenuFactory"/> default implementation -->
        /// </summary>
        /// <param name="title"></param>
        public WindowBuilder WithChromeBar(object chromeFactory)
        {
            return this;
        }

        /// <summary>
        /// Adds a minimum With and Height to the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="minWidth"></param>
        /// <param name="minHeight"></param>
        public WindowBuilder WithMinBounds(double minWidth, double minHeight)
        {
            Window.MinWidth = minWidth;
            Window.MinHeight = minHeight;
            return this;
        }

        /// <summary>
        /// Adds a maximum With and Height to the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        public WindowBuilder WithMaxBounds(double maxWidth, double maxHeight)
        {
            Window.MaxWidth = maxWidth;
            Window.MaxHeight = maxHeight;
            return this;
        }

        /// <summary>
        /// Adds window rules to the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="canResize"></param>
        /// <param name="canMinimize"></param>
        /// <param name="showInTaskbar"></param>
        public WindowBuilder WithWindowRules(bool canResize = false, bool canMinimize = true, bool showInTaskbar = true)
        {
            Window.CanResize = canResize;
            Window.Fullscreen.IsVisible = canResize;
            Window.Minimize.IsVisible = canMinimize;
            Window.ShowInTaskbar = showInTaskbar;
            return this;
        }

        /// <summary>
        /// Builds the current <see cref="WindowBuilder"/> into a <see cref="GenericWindow"/> control
        /// </summary>
        /// <returns></returns>
        public Window Build()
        {
            // Initialize chrome events
            Window.Minimize.Click += (s, e) => Window.WindowState = WindowState.Minimized;
            Window.Fullscreen.Click += (s, e) => Window.WindowState = Window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            Window.Quit.Click += (s, e) => {
                Window.Result = DialogResult.Cancel;
                Window.Close();
            };

            // Set OnLoad events
            Window.Loaded += (s, e) => {
                Window.MinWidth = Window.MinWidth == 0 ? Window.Bounds.Width : Window.MinWidth;
                Window.MinHeight = Window.MinHeight == 0 ? Window.Bounds.Height : Window.MinHeight;
            };

            // Check icon
            if (Window.Icon == null) {
                Window.DialogIcon.Source = App.Icon ?? App.DefaultIcon;
                Window.Icon = new((IBitmap)Window.DialogIcon.Source);
            }

            // Return built window
            return Window;
        }
    }
}
