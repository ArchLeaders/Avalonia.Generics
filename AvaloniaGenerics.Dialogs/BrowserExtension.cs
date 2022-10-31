using Avalonia.Platform.Storage;

namespace AvaloniaGenerics.Dialogs
{
    public static class BrowserExtension
    {
        public static IStorageFolder? LastSelectedDirectory { get; set; }
        public static IStorageFolder? LastSaveDirectory { get; set; }

        public static async Task<string?> ShowDialog(this BrowserDialog browser, string title = "")
        {
            string? path = null;

            if (browser == BrowserDialog.Folder) {
                var result = await GetTopLevel()!.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions() {
                    Title = title,
                    SuggestedStartLocation = LastSelectedDirectory
                });

                IStorageItem? item = result.FirstOrDefault() is IStorageItem _item ? _item : null;
                if (item != null) {
                    path = item.TryGetUri(out Uri? uri) ? uri.ToString() : item.Name;
                    LastSelectedDirectory = item as IStorageFolder;
                }
            }
            else if (browser == BrowserDialog.File) {
                var result = await GetTopLevel()!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions() {
                    Title = title,
                    SuggestedStartLocation = LastSelectedDirectory
                });

                IStorageItem? item = result.FirstOrDefault() is IStorageItem _item ? _item : null;
                if (item != null) {
                    path = item.TryGetUri(out Uri? uri) ? uri.ToString() : item.Name;
                    LastSelectedDirectory = await item.GetParentAsync();
                }
            }
            else {
                var result = await GetTopLevel()!.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions() {
                    Title = title,
                    SuggestedStartLocation = LastSaveDirectory
                });
                path = result != null ? result.TryGetUri(out Uri? uri) ? uri.ToString() : result.Name : null;
                LastSaveDirectory = result != null ? await result.GetParentAsync() : null;
            }

            return path?.Remove(0, 8);
        }
    }
}
