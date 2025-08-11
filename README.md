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
git clone https://github.com/romanwit/ToDoListWpf.git
cd ToDoListWpf
```

### 2. Run the application

Please open the solution in Visual Studio and run it. NuGet packages will be install automatically (if they were not). Project already contain migration.

### Project Structure

**Domain** — Entities and domain interfaces

**Application** — DTOs, service interfaces, business logic

**Infrastructure** — Data access implementations, EF Core migrations, PDF export service

**Presentation** — WPF UI, MVVM ViewModels, Views

### Architecture solutions

We use here clean architecture with separation of layers of abstraction. To use concrete implementations, we use dependency injection and automapper.

Saving to filesystem (JSON or CSV) can cause a problems with performance in real life projects, so some relational DB should be used. So, data is stored in SQLite, it is most obvious for such project.

We use Guid as id of task; if we shall know in advance, that we shall use RDBMS, we should use an autoincrement at DB (Guid takes too much space). But in the domain layer we do not know about concrete implementation. It can be saving to filesystem, or Redis.

For GUI we use MVVM pattern, that is standard for WPF. There are just 2 sets of views and viewmodels: MainWindow to show all tasks and TaskEdit to Add/Edit a task. Connection between XAML and viewmodels is made by RelayCommands, that is also standard for WPF.

GUI have a filter just by isCompleted property and export to Pdf with using of QuestPdf library.

Title should be not null, so there is a small validation in TaskEdit window.

### License
This project is open-source and free to use and modify.


