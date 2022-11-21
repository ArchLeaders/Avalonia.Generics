using Avalonia.Controls;
using Avalonia.Generics.Builders;
using Avalonia.Generics.Controls;

namespace Avalonia.Generics.Dialogs
{
    public partial class InputDialog : UserControl
    {
        private Dictionary<string, string> InputMap { get; set; } = new();

        public InputDialog() { }
        public InputDialog(Dictionary<string, string> inputMap)
        {
            InitializeComponent();
            InputMap = inputMap;

            foreach ((var key, var value) in InputMap) {

                var tb = new TextBox() {
                    Watermark = key,
                    Text = value,
                    Margin = new Thickness(0, 0, 0, 15),
                    UseFloatingWatermark = true
                };

                tb.GetObservable(TextBlock.TextProperty).Subscribe(x => InputMap[key] = x!);
                Root.Children.Add(tb);
            }
        }

        public static async Task<Dictionary<string, string>?> ShowDialog(Dictionary<string, string> inputMap, string title, DialogButtons dialogButtons = DialogButtons.OkCancel, WindowOptions? options = null)
        {
            options ??= WindowOptions.Dialog;
            InputDialog dialog = new(inputMap);
            GenericWindow window = new WindowBuilder()
                .WithContent(dialog)
                .WithTitle(title)
                .WithWindowOptions(options)
                .WithMaxBounds(options.CanResize ? double.NaN : 250, options.CanResize ? double.NaN : 300)
                .WithMinBounds(250, 300)
                .WithDialogButtons(dialogButtons)
                .Build();

            await window.ShowDialog(App.View);

            if (window.Result != DialogResult.Cancel && window.Result != DialogResult.No) {
                return dialog.InputMap;
            }

            return null;
        }
    }
}
