# Avalonia Generics

**Avalonia Generics** is a helper libray that implements commonly used functions and extensions like `MessageBox` and `BrowserDialog`.

## Usage

The library needs to be initialized with the desktop window instance to attach dialogs to, that can be done in the `App.axaml.cs` file as follows.

```cs
if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
    AppView view = new();

    desktop.MainWindow = view {
        DataContext = new AppViewModel(),
    };

    // Initialize the Dialogs library here
    ApplicationLoader.Attach(this);
}
```

<br>

## Install

Install with NuGet or build from [source](https://github.com/ArchLeaders/AvaloniaGenerics/tree/master/AvaloniaGenerics.Dialogs).

#### NuGet
```powershell
Install-Package AvaloniaGenerics
```

#### Build from source
```batch
git clone https://github.com/ArchLeaders/Avalonia.Generics.git
dotnet build Avalonia.Generics/Avalonia.Generics/
```

---

**© Arch Leaders**