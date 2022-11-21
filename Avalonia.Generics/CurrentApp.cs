#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

global using static Avalonia.Generics.CurrentApp;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Avalonia.Generics
{
    internal class CurrentApp
    {
        internal static CurrentApp App { get; set; } = new();

        internal Window View { get; set; }
        internal Bitmap? Icon { get; set; }
        internal Bitmap DefaultIcon { get; set; } = new Bitmap(AvaloniaLocator.Current.GetService<IAssetLoader>()!.Open(new($"avares://Avalonia.Generics/Assets/avalonia.ico")));
        internal TopLevel TopLevel { get; set; } = null!;
    }
}
