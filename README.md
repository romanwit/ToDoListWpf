# ToDoList

ToDoList is a simple task management application featuring task creation, editing, deletion, filtering, and PDF export capabilities.

---

## Features

- View a list of tasks with filtering options (All, Completed, Not Completed)
- Add, edit, and delete tasks via a dedicated dialog window
- Export the current task list to a PDF file
- Implements MVVM architecture with Domain, Application, Infrastructure, and Presentation layers
- Uses SQLite database with Entity Framework Core migrations

---

## Requirements

- .NET 6.0 or higher
- Visual Studio 2022 or any compatible IDE
- SQLite

---

## Setup and Run

### 1. Clone the repository

```powershell
git clone https://github.com/romanwit/ToDoList.git
cd ToDoList
```

### 2. Install NuGet packages

Navigate to the solution folder and run these commands in the terminal, or install via Visual Studio NuGet Package Manager:

```powershell
dotnet restore
```

### 3.  Create and apply database migrations

Go to the Infrastructure project directory (where ToDoDbContext is located):

```powershell
cd ToDoList.Infrastructure
```

Create the initial migration (if not created yet):

```powershell
dotnet ef migrations add InitialCreate --startup-project ../ToDoList.Presentation
```

Apply the migration to create/update the database:

```powershell
dotnet ef database update --startup-project ../ToDoList.Presentation
```

### 4. Run the application

Return to the solution root and run the Presentation project

```powershell
cd ../ToDoList.Presentation
dotnet run
```

### Project Structure
**Domain** — Entities and domain interfaces

**Application** — DTOs, service interfaces, business logic

**Infrastructure** — Data access implementations, EF Core migrations, PDF export service

**Presentation** — WPF UI, MVVM ViewModels, Views

### License
This project is open-source and free to use and modify.


