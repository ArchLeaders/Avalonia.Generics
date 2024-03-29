﻿using Avalonia.Generics.Dialogs;
using Avalonia.Generics.Factories;

namespace Avalonia.Generics.Demo.Models
{
    public class MenuModel
    {
        [Menu("Open", "_File", "Ctrl + O", Icon = "fa-solid fa-book")]
        public async void Open()
        {
            await MessageBox.ShowDialog("Rich **markdown** *formatting*! (With [hyperlinks](https://google.com))", "Markdown", formatting: Formatting.Markdown);
        }
    }
}
