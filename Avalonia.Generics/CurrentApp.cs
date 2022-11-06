global using static AvaloniaGenerics.CurrentApp;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaGenerics
{
    internal class CurrentApp
    {
        internal static CurrentApp App { get; set; } = new();

        internal Window View { get; set; }
        internal Bitmap? Icon { get; set; }
        internal Bitmap DefaultIcon { get; set; } = null!;
        internal TopLevel TopLevel { get; set; } = null!;
    }
}
