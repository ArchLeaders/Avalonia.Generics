using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Markdown.Avalonia;
using Material.Icons;

namespace Avalonia.Generics.Dialogs
{
    public enum MessageBoxButtons { Ok, OkCancel, YesNo, YesNoCancel }
    public enum DialogResult { Cancel, No, Ok, Yes }
    public enum Formatting { None, Markdown }

    public partial class MessageBox : Window
    {
        public MessageBox() => AvaloniaXamlLoader.Load(this);
        public MessageBox(string title, string text, Formatting formatting, MaterialIconKind? icon = null)
        {
            AvaloniaXamlLoader.Load(this);

            if (icon != null) {
                this.FindControl<Image>("DefaultIco")!.IsVisible = false;
                Material.Icons.Avalonia.MaterialIcon materialIco = this.FindControl<Material.Icons.Avalonia.MaterialIcon>("MaterialIco")!;
                materialIco.IsVisible = true;
                materialIco.Kind = (MaterialIconKind)icon;
            }

            var tb = this.FindControl<TextBox>("Text")!;
            if (formatting == Formatting.Markdown) {
                tb.IsVisible = false;

                var mdViewer = this.FindControl<MarkdownScrollViewer>("Markdown")!;
                mdViewer.IsVisible = true;
                mdViewer.Markdown = text;
            }
            else {
                tb.Text = text;
            }

            this.FindControl<TextBlock>("TitleBox")!.Text = title;
            this.FindControl<Button>("Copy")!.Click += async (_, _) => {
                if (formatting == Formatting.Markdown) {
                    await Application.Current!.Clipboard!.SetTextAsync($"**{title}**\n\n{text}");
                    return;
                }

                await Application.Current!.Clipboard!.SetTextAsync($"**{title}**\n```\n{text}\n```");
            };
        }

        public static void ShowSync(string text, string title = "Notice", MessageBoxButtons buttons = MessageBoxButtons.Ok, Formatting formatting = Formatting.None, MaterialIconKind? icon = null)
        {
            using var source = new CancellationTokenSource();
            Show(text, title, buttons, formatting, icon).ContinueWith(t => source.Cancel(), TaskScheduler.FromCurrentSynchronizationContext());
            Dispatcher.UIThread.MainLoop(source.Token);
        }

        public static async Task<DialogResult> Show(string text, string title = "Notice", MessageBoxButtons buttons = MessageBoxButtons.Ok, Formatting formatting = Formatting.None, MaterialIconKind? icon = null)
        {
            MessageBox msgbox = new(title, text, formatting, icon);
            var res = DialogResult.Ok;

            var buttonPanel = msgbox.FindControl<StackPanel>("Buttons")!;
            var close = msgbox.FindControl<Button>("Close")!;

            close.Click += (_, __) => {
                res = DialogResult.Cancel;
                msgbox.Close();
            };

            void AddBtn(string caption, DialogResult r, bool def = false, int mode = 0)
            {
                var btn = new Button {
                    Content = caption,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(10, 0, 0, 0),
                    MinWidth = 67,
                    IsDefault = mode == 1,
                    IsCancel = mode == 2
                };

                btn.Click += (_, __) => {
                    res = r;
                    msgbox.Close();
                };

                buttonPanel.Children.Add(btn);
                if (def) res = r;
            }

            if (buttons == MessageBoxButtons.Ok || buttons == MessageBoxButtons.OkCancel)
                AddBtn("Ok", DialogResult.Ok, true, 1);

            if (buttons == MessageBoxButtons.YesNo || buttons == MessageBoxButtons.YesNoCancel) {
                AddBtn("Yes", DialogResult.Yes, mode: 1);
                AddBtn("No", DialogResult.No, true);
            }

            if (buttons == MessageBoxButtons.OkCancel || buttons == MessageBoxButtons.YesNoCancel)
                AddBtn("Cancel", DialogResult.Cancel, true, 2);


            var tcs = new TaskCompletionSource<DialogResult>();
            msgbox.Closed += delegate { tcs.TrySetResult(res); };

            if (App.View != null) {
                await msgbox.ShowDialog(App.View);
            }
            else {
                msgbox.Show();
            }

            return await tcs.Task;
        }
    }
}
