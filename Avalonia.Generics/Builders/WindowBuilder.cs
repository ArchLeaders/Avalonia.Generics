using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Generics.Controls;
using Avalonia.Generics.Dialogs;
using Avalonia.Generics.Extensions;
using Avalonia.Generics.Factories;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Material.Icons;

namespace Avalonia.Generics.Builders
{
    public class WindowBuilder
    {
        private readonly GenericWindow Window = new();

        /// <summary>
        /// Creates a new <see cref="WindowBuilder"/> instance with the <paramref name="baseWindow"/> properties
        /// </summary>
        public static WindowBuilder Initialize(Window baseWindow)
        {
            WindowBuilder windowBuilder = new();
            windowBuilder.Window.DataContext = baseWindow.DataContext ?? baseWindow;
            return windowBuilder
                .WithDefaultBounds(baseWindow.Width, baseWindow.Height)
                .WithMinBounds(baseWindow.MinWidth, baseWindow.MinHeight)
                .WithMaxBounds(baseWindow.MaxWidth, baseWindow.MaxHeight)
                .WithTitle(baseWindow.Title ?? "")
                .WithIcon(baseWindow.Icon)
                .WithWindowOptions(WindowOptions.Window)
                .WithContent(baseWindow.Content ?? new TextBlock() {
                    Text = "No Content Loaded",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                });
        }

        /// <summary>
        /// Creates a new <see cref="WindowBuilder"/> instance
        /// </summary>
        public static WindowBuilder Initialize()
        {
            return new();
        }

        /// <summary>
        /// Adds a <see cref="string"/> Title to the current <see cref="WindowBuilder"/>
        /// </summary>
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
        public WindowBuilder WithIcon(WindowIcon? icon)
        {
            Window.DialogIcon.Source = ApplicationLoader.GetWindowIcon(icon) ?? App.DefaultIcon;
            Window.Icon = icon;
            return this;
        }

        /// <summary>
        /// Adds an <see cref="IImage"/> icon to the current <see cref="WindowBuilder"/>
        /// </summary>
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
        public WindowBuilder WithContent(object content)
        {
            Window.Content.Content = content;
            return this;
        }

        /// <summary>
        /// Adds a Menu object to the current <see cref="WindowBuilder"/> to be parsed by the <see cref="MenuFactory.MenuFactory"/> default implementation
        /// </summary>
        /// <param name="menuFactoryModel">A class instance defining the <see cref="MenuItem"/>s as <see cref="MenuFactory.Attributes.MenuAttribute"/> attributed methods</param>
        public WindowBuilder WithMenu(object menuFactoryModel)
        {
            Window.TitleBox.IsVisible = false;
            Window.RootMenu.Items = MenuFactory.MenuFactory.Generate(menuFactoryModel);
            return this;
        }

        /// <summary>
        /// <!-- Adds a Menu object to the current <see cref="WindowBuilder"/> to be parsed by the <see cref="MenuFactory.MenuFactory"/> default implementation -->
        /// </summary>
        /// <param name="title"></param>
        public WindowBuilder WithChromeBar(object chromeFactoryModel)
        {
            ChromeFactory.Generate(Window, chromeFactoryModel);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="click"></param>
        /// <param name="mode">1 = IsDefault<para>2 = IsCancel</para></param>
        /// <returns></returns>
        public WindowBuilder WithButton(string content, EventHandler<RoutedEventArgs> click, int mode = 0)
        {
            Window.ButtonStack.IsVisible = true;
            var button = new Button {
                Content = content, IsDefault = mode == 1, IsCancel = mode == 2
            };
            button.Click += click;
            Window.ButtonStack.Children.Add(button);
            return this;
        }

        /// <summary>
        /// Adds the DialogButtons set to the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="title"></param>
        public WindowBuilder WithDialogButtons(DialogButtons buttons)
        {
            if (buttons == DialogButtons.Ok || buttons == DialogButtons.OkCancel)
                WithButton(DialogResult.Ok.ToString(), (s, e) => {
                    Window.Result = DialogResult.Ok;
                    Window.Close();
                }, 1);

            if (buttons == DialogButtons.YesNo || buttons == DialogButtons.YesNoCancel) {
                WithButton(DialogResult.Yes.ToString(), (s, e) => {
                    Window.Result = DialogResult.Yes;
                    Window.Close();
                }, 1);
                WithButton(DialogResult.No.ToString(), (s, e) => {
                    Window.Result = DialogResult.No;
                    Window.Close();
                }, 0);
            }

            if (buttons == DialogButtons.OkCancel || buttons == DialogButtons.YesNoCancel) {
                WithButton(DialogResult.Cancel.ToString(), (s, e) => {
                    Window.Result = DialogResult.Cancel;
                    Window.Close();
                }, 2);
            }

            return this;
        }

        /// <summary>
        /// Adds a default With and Height to the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public WindowBuilder WithDefaultBounds(double width, double height)
        {
            Window.Width = width;
            Window.Height = height;
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
        /// Adds window options to the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="canResize"></param>
        /// <param name="canMinimize"></param>
        /// <param name="showInTaskbar"></param>
        public WindowBuilder WithWindowOptions(bool canResize = false, bool canMinimize = true, bool showInTaskbar = true, bool canClose = true, SizeToContent sizeToContent = SizeToContent.Manual)
        {
            Window.CanResize = canResize;
            Window.Fullscreen.IsVisible = canResize;
            Window.Minimize.IsVisible = canMinimize;
            Window.ShowInTaskbar = showInTaskbar;
            Window.Quit.IsVisible = canClose;
            Window.SizeToContent = sizeToContent;
            return this;
        }

        /// <summary>
        /// Adds window options to the current <see cref="WindowBuilder"/>
        /// </summary>
        public WindowBuilder WithWindowOptions(WindowOptions options)
        {
            Window.CanResize = options.CanResize;
            Window.Fullscreen.IsVisible = options.CanResize;
            Window.Minimize.IsVisible = options.CanMinimize;
            Window.ShowInTaskbar = options.ShowInTaskbar;
            Window.Quit.IsVisible = options.CanClose;
            Window.SizeToContent = options.SizeToContent;
            return this;
        }

        /// <summary>
        /// Sets the background and chrome color of the current <see cref="WindowBuilder"/>
        /// </summary>
        /// <param name="backgroundColor"></param>
        /// <param name="chromeColor"></param>
        /// <param name="chromeOpacity"></param>
        public WindowBuilder WithWindowColors(string backgroundColor, string chromeColor, double chromeOpacity = 1.0D)
        {
            if (backgroundColor.Contains('#')) {
                Window.Background = backgroundColor.GetBrush();
            }
            else {
                Window[!TemplatedControl.BackgroundProperty] = backgroundColor.GetResource();
            }

            if (chromeColor.Contains('#')) {
                Window.Chrome.Background = chromeColor.GetBrush();
            }
            else {
                Window.Chrome[!TemplatedControl.BackgroundProperty] = backgroundColor.GetResource();
            }

            Window.Chrome.Opacity = chromeOpacity;
            return this;
        }

        /// <summary>
        /// Builds the current <see cref="WindowBuilder"/> into a <see cref="GenericWindow"/> control
        /// </summary>
        /// <returns></returns>
        public GenericWindow Build()
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
