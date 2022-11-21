using Avalonia.Generics.Dialogs;
using Avalonia.MenuFactory.Attributes;
using Material.Icons;

namespace Avalonia.Generics.Demo.Models
{
    public class MenuModel
    {
        [Menu("Open", "_File", "Ctrl + O", Icon = MaterialIconKind.FolderOpenOutline)]
        public async void Open()
        {
            await MessageBox.ShowDialog("Rich **markdown** *formatting*! (With [hyperlinks](https://google.com))", "Markdown", formatting: Formatting.Markdown);
        }
    }
}
