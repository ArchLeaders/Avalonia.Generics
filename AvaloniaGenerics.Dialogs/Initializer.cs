global using static AvaloniaGenerics.Dialogs.Initializer;

using Avalonia.Controls;
using Avalonia.VisualTree;

namespace AvaloniaGenerics.Dialogs
{
    public static class Initializer
    {
        internal static Window? View { get; set; }

        internal static TopLevel? GetTopLevel()
        {
            if (View != null) {
                return View.GetVisualRoot() as TopLevel;
            }
            else {
                throw new Exception($"Could not find visual root in library '{nameof(Dialogs)}'. Make sure {nameof(Dialogs)} is intialized with 'IVisual.InitializeGenericDialogs()'.");
            }
        }

        public static void InitializeGenericDialogs(Window view) => View = view;
    }
}
