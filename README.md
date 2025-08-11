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

### License
This project is open-source and free to use and modify.


