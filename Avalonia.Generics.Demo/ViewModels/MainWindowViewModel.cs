using Avalonia.Controls;
using Avalonia.Generics.Builders;
using Avalonia.Generics.Controls;
using Avalonia.Generics.Demo.Models;
using Avalonia.Generics.Dialogs;
using Avalonia.Layout;
using System.Linq;

namespace Avalonia.Generics.Demo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public async void Click()
        {
            string? str = await new BrowserDialog(BrowserMode.OpenFile, "Opne some file", "Actor Packs:*.sbactorpack;*.bactorpack").ShowDialog();
            await MessageBox.ShowDialog(str ?? "null");

            GenericWindow window = new WindowBuilder()
                .WithTitle("Hello World")
                .WithContent(new Button() {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Content = "Click Me!"
                })
                .WithMinBounds(300, 200)
                .WithMaxBounds(500, 400)
                .WithMenu(new MenuModel())
                .WithWindowColors("SystemChromeLowColor", "#FFFFFF", 0.4)
                .WithButton("Yeet", async (s, e) => {
                    await MessageBox.ShowDialog("Hola senior");
                })
                .Build();

            await window.ShowDialog();

            var thing = await InputDialog.ShowDialog(new() {
                { "InputA", "" },
                { "InputB", "" },
                { "InputC", "" },
                { "InputD", "" },
                { "InputE", "" },
                { "InputF", "" },
                { "InputG", "" },
                { "InputH", "" },
                { "InputI", "" },
            }, "Input Dialog", DialogButtons.OkCancel);

            await MessageBox.ShowDialog(string.Join('\n', thing?.Select(x => $"{x.Key}: {x.Value}") ?? new string[] { "null" }), "Sample");

            await MessageBox.ShowDialog("Some sample text", "Sample");
        }
    }
}
