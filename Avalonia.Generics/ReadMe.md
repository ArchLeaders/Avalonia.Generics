# Avalonia Generics - Dialogs

**Avalonia Generics - Dialogs** is a helper libray that implements commonly used Dialog functions and Extensions like `MessageBox` and `BrowserDialog`.

## Usage

The library needs to be initialized with the desktop window instance to attach the dialogs to, that can be done in the `App.axaml.cs` file as follows.

```cs
if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
    AppView view = new();

    desktop.MainWindow = view {
        DataContext = new AppViewModel(),
    };

    // Initialize the Dialogs library here
    view.InitializeGenericDialogs();
}
```

<br>

## Install

Install with NuGet or build from [source](https://github.com/ArchLeaders/AvaloniaGenerics/tree/master/AvaloniaGenerics.Dialogs).

#### NuGet
```powershell
Install-Package AvaloniaGenerics.Dialogs
```

#### Build from source
```batch
git clone https://github.com/ArchLeaders/AvaloniaGenerics.git
dotnet build AvaloniaGenerics/AvaloniaGenerics.Dialogs
```

---

**© Arch Leaders**