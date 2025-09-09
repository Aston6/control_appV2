# MyApp2

## Overview

MyApp2 is a cross-platform desktop application built with Avalonia UI and .NET, featuring a modular architecture for control panel operations, user management, reporting, and more. The solution includes both the main application and a test project.

## Project Structure

```
MyApp2.sln
MyApp2_All/
  ├── App.axaml / App.axaml.cs
  ├── app.db
  ├── app.manifest
  ├── MyApp2.csproj
  ├── Program.cs
  ├── ViewLocator.cs
  ├── Images/
  ├── Library/
  │   ├── Data/
  │   ├── Models/
  │   ├── Service/
  │   └── ViewModel/
  ├── Migrations/
  ├── Services/
  ├── Views/
  ├── bin/
  └── obj/
MyApp2.TestCase/
  ├── MyApp2.TestCase.csproj
  ├── UnitTest1.cs
  ├── bin/
  └── obj/
```

## Features

- **MVVM Architecture**: ViewModels for each major feature (Control Panel, Buy List, Reports, etc.).
- **Database**: Uses SQLite via Entity Framework Core ([app.db](MyApp2_All/app.db)).
- **Navigation**: Centralized navigation service.
- **Testing**: Unit tests in [MyApp2.TestCase](MyApp2.TestCase/UnitTest1.cs).
- **Extensible**: Easily add new views, services, and models.

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Avalonia UI](https://avaloniaui.net/)
- (Optional) Visual Studio or VS Code

### Build & Run

1. **Restore dependencies:**
   ```sh
   dotnet restore MyApp2_All/MyApp2.csproj
   ```

2. **Build the project:**
   ```sh
   dotnet build MyApp2_All/MyApp2.csproj
   ```

3. **Run the application:**
   ```sh
   dotnet run --project MyApp2_All/MyApp2.csproj
   ```

### Database Migrations

Entity Framework Core migrations are stored in [MyApp2_All/Migrations](MyApp2_All/Migrations/). On first run, the database is created and seeded automatically.

## Testing

Unit tests are located in [MyApp2.TestCase/UnitTest1.cs](MyApp2.TestCase/UnitTest1.cs):

```sh
dotnet test MyApp2.TestCase/MyApp2.TestCase.csproj
```

## Folder Details

- **Library/ViewModel/**: Main ViewModels (e.g., [`ControlPanelViewModel`](MyApp2_All/Library/ViewModel/ControlPanelViewModel.cs), [`MainViewModel`](MyApp2_All/Library/ViewModel/MainViewModel.cs))
- **Library/Service/**: Service classes (e.g., [`MotionPara`](MyApp2_All/Library/Service/MotionPara.cs))
- **Views/**: Avalonia XAML views (e.g., [`MainView.axaml.cs`](MyApp2_All/Views/MainView.axaml.cs))
- **Services/**: Application-wide services (navigation, user, image picker, etc.)
- **Migrations/**: EF Core migration files

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Submit a pull request



For more details, see the source files linked above.
