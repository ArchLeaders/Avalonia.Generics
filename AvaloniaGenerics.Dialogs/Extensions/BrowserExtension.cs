using Avalonia.Input;
using Avalonia.Platform.Storage;

namespace Avalonia.Generics.Extensions
{
    public enum BrowserDialog { OpenFile, OpenFolder, SaveFile }

    public static class BrowserExtension
    {
        public static IStorageFolder? LastSelectedDirectory { get; set; }
        public static IStorageFolder? LastSaveDirectory { get; set; }

        /// <inheritdoc cref="ShowDialog(BrowserDialog, string?, string?, bool)"/>
        public static async Task<string?> ShowDialog(this BrowserDialog browser, string? title = null, string? filter = null)
        {
            return (await browser.ShowDialog(title, filter, false))?.First();
        }

        /// <summary>
        /// Opens a new <paramref name="browser"/> dialog and returns the selected files/folders.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="title"></param>
        /// <param name="filter">Semicolon delimited list of file filters. (Syntax: <c>Yaml Files<see cref="char">:</see>*.yml<see cref="char">;</see>*.yaml<see cref="char">|</see>All Files<see cref="char">:</see>*.*</c>)</param>
        /// <param name="multiple"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>?> ShowDialog(this BrowserDialog browser, string? title = null, string? filter = null, bool multiple = true)
        {
            title ??= browser.ToString().Replace("F", " F");

            IStorageProvider StorageProvider = App.GetTopLevel().StorageProvider;

            object? result = browser switch
            {
                BrowserDialog.OpenFolder => await StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
                {
                    Title = title,
                    SuggestedStartLocation = LastSelectedDirectory,
                    AllowMultiple = multiple
                }),
                BrowserDialog.OpenFile => await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
                {
                    Title = title,
                    SuggestedStartLocation = LastSelectedDirectory,
                    AllowMultiple = multiple,
                    FileTypeFilter = LoadFileBrowserFilter(filter)
                }),
                BrowserDialog.SaveFile => await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions()
                {
                    Title = title,
                    SuggestedStartLocation = LastSaveDirectory,
                    FileTypeChoices = LoadFileBrowserFilter(filter)
                }),
                _ => throw new NotImplementedException()
            };

            if (result is IReadOnlyList<IStorageFolder> folders && folders.Count > 0)
            {
                LastSelectedDirectory = folders[folders.Count - 1];
                return folders.Select(folder => folder.TryGetUri(out Uri? uri) ? uri.AbsoluteUri : folder.Name);
            }
            else if (result is IReadOnlyList<IStorageFile> files && files.Count > 0)
            {
                LastSelectedDirectory = await files[files.Count - 1].GetParentAsync();
                return files.Select(file => file.TryGetUri(out Uri? uri) ? uri.AbsoluteUri : file.Name);
            }
            else if (result is IStorageFile file)
            {
                LastSelectedDirectory = await file.GetParentAsync();
                return new string[1] {
                    file.TryGetUri(out Uri? uri) ? uri.AbsoluteUri : file.Name
                };
            }
            else
            {
                return null;
            }
        }

        internal static FilePickerFileType[] LoadFileBrowserFilter(string? filter = null)
        {
            if (filter != null)
            {
                try
                {
                    string[] groups = filter.Split('|');
                    FilePickerFileType[] types = new FilePickerFileType[groups.Length];

                    for (int i = 0; i < groups.Length; i++)
                    {
                        string[] pair = groups[i].Split(':');
                        types[i] = new(pair[0])
                        {
                            Patterns = pair[1].Split(';')
                        };
                    }
                }
                catch
                {
                    throw new FormatException(
                        $"Could not parse filter arguments '{filter}'.\n" +
                        $"Example: \"Yaml Files:*.yml;*.yaml|All Files:*.*\"."
                    );
                }
            }

            return Array.Empty<FilePickerFileType>();
        }
    }
}
