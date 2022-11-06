using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Generics.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.Generics.Demo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public void Click()
        {
            GenericDialog dialog = new("Hello World", new Button() {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = "Click Me!"
            }, true, true, 300, 200);
            dialog.Show();
        }
    }
}