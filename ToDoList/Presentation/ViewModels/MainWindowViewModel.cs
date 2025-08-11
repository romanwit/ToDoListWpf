using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ToDoList.Application.DTOs;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Interfaces;
using ToDoList.Presentation.Models;
using ToDoList.Presentation.Views;

namespace ToDoList.Presentation.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPdfExporter _pdfExportService;
        private readonly IMapper _mapper;
        private string _filter = "All";
        private TaskModel? _selectedTask;

        public MainWindowViewModel(IMapper mapper, ITaskRepository taskRepository, IPdfExporter pdfExportService)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _pdfExportService = pdfExportService;

            LoadTasksCommand = new RelayCommand(LoadTasks);
            AddTaskCommand = new RelayCommand(AddTask);
            EditTaskCommand = new RelayCommand(EditTask, () => SelectedTask != null);
            DeleteTaskCommand = new RelayCommand(DeleteTask, () => SelectedTask != null);
            ExportPdfCommand = new RelayCommand(ExportPdf);

            LoadTasks();
        }

        public ObservableCollection<TaskModel> Tasks { get; set; } = new();
        public ObservableCollection<TaskModel> FilteredTasks { get; set; } = new();
        public ICommand LoadTasksCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand ExportPdfCommand { get; }
        public TaskModel? SelectedTask
        {
            get => _selectedTask;
            set
            {
                SetProperty(ref _selectedTask, value);
                ((RelayCommand)EditTaskCommand).NotifyCanExecuteChanged();
                ((RelayCommand)DeleteTaskCommand).NotifyCanExecuteChanged();
            }
        }
   
        public string Filter
        {
            get => _filter;
            set
            {
                if (SetProperty(ref _filter, value))
                    ApplyFilter();
            }
        }

        private async void LoadTasks()
        {
            Tasks.Clear();
            var taskEntities = await _taskRepository.GetAllAsync();
            foreach (var entity in taskEntities)
            {
                var taskModel = new TaskModel(entity);
                Tasks.Add(taskModel);
            }
            ApplyFilter();
        }

        private async void AddTask()
        {
            var vm = new TaskEditViewModel(); 
            var window = new TaskEditWindow { DataContext = vm };

            window.Owner = System.Windows.Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (window.ShowDialog() == true)
            {
                var dto = vm.ToDto();
                var entity = _mapper.Map<TodoTask>(dto);
                await _taskRepository.AddAsync(entity);
                Tasks.Add(new TaskModel(entity));
            }
        }

        private async void EditTask()
        {
            if (SelectedTask == null) return;

            var vm = new TaskEditViewModel(SelectedTask);
            var window = new TaskEditWindow { DataContext = vm };

            window.Owner = System.Windows.Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (window.ShowDialog() == true)
            {
                var dto = vm.ToDto();
                var entity = _mapper.Map<TodoTask>(dto);
                await _taskRepository.UpdateAsync(entity);

                var index = Tasks.IndexOf(SelectedTask);
                Tasks[index] = new TaskModel(entity);
            }
        }

        private async void DeleteTask()
        {
            if (SelectedTask == null) return;

            await _taskRepository.DeleteAsync(SelectedTask.Id);
            Tasks.Remove(SelectedTask);
            LoadTasks();
        }

        private void ExportPdf()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = "tasks.pdf",
                Title = "Save tasks as PDF"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var dtos = FilteredTasks.Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    CreatedAt = t.CreatedAt,
                    IsCompleted = t.IsCompleted,
                    Priority = t.Priority
                }).ToList();

                _pdfExportService.ExportTasksToPdf(dtos, saveFileDialog.FileName);
            }
        }

        private void ApplyFilter()
        {
            if (Filter == "Completed")
                FilteredTasks = new ObservableCollection<TaskModel>(Tasks.Where(t => t.IsCompleted));
            else if (Filter == "Not Completed")
                FilteredTasks = new ObservableCollection<TaskModel>(Tasks.Where(t => !t.IsCompleted));
            else
                FilteredTasks = Tasks;

            OnPropertyChanged(nameof(FilteredTasks));
        }
    }
}
