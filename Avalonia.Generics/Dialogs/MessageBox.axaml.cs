using Avalonia.Controls;
using Avalonia.Generics.Builders;
using Avalonia.Generics.Controls;
using Avalonia.Generics.Factories;
using Material.Icons;

namespace Avalonia.Generics.Dialogs
{
    public partial class MessageBox : UserControl
    {
        private Formatting Formatting { get; } = Formatting.None;

        [Chrome(MaterialIconKind.ContentCopy)]
        public async void Copy(Window window)
        {
            string text = Formatting == Formatting.Markdown ? $"\n{MarkdownViewer.Markdown}" : $"```\n{TextViewer.Text}\n```";
            await Application.Current!.Clipboard!.SetTextAsync($"**{window.Title}**\n{text}");
        }

        public MessageBox() { }
        public MessageBox(string text, Formatting formatting = Formatting.None)
        {
            InitializeComponent();
            Formatting = formatting;

            if (formatting == Formatting.Markdown) {
                TextViewer.IsVisible = false;
                MarkdownViewer.IsVisible = true;
                MarkdownViewer.Markdown = text;
            }
            else {
                TextViewer.Text = text;
            }
        }

        public static async Task<DialogResult> ShowDialog(string text, string title = "Notice", DialogButtons dialogButtons = DialogButtons.Ok, Formatting formatting = Formatting.None, WindowOptions? options = null)
        {
            options ??= WindowOptions.Dialog;
            MessageBox dialog = new(text, formatting);
            GenericWindow window = new WindowBuilder()
                .WithContent(dialog)
                .WithTitle(title)
                .WithWindowOptions(options)
                .WithMaxBounds(options.CanResize ? double.NaN : 400, options.CanResize ? double.NaN : 300)
                .WithMinBounds(200, 120)
                .WithDialogButtons(dialogButtons)
                .WithChromeBar(dialog)
                .Build();

            await window.ShowDialog(App.View);

            return window.Result;
        }
    }
}
