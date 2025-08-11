using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Windows;
using ToDoList.Application.DTOs;
using ToDoList.Domain.Enums;
using ToDoList.Presentation.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ToDoList.Presentation.ViewModels
{
    public class TaskEditViewModel : ViewModelBase, IDataErrorInfo
    {
        public Guid Id { get; set; }
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (SetProperty(ref _title, value))
                {
                    OnPropertyChanged(nameof(HasErrors));
                }
            }
        }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; } = Priority.Normal;
        public List<Priority> Priorities { get; } = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Title))
                {
                    if (string.IsNullOrWhiteSpace(Title))
                        return "Title cannot be empty";
                }
                return null;
            }
        }

        public bool HasErrors => !string.IsNullOrEmpty(this[nameof(Title)]);

        public TaskEditViewModel()
        {
            OkCommand = new RelayCommand(() => CloseDialog(true));
            CancelCommand = new RelayCommand(() => CloseDialog(false));
        }

        public TaskEditViewModel(TaskModel model) : this()
        {
            Id = model.Id;
            Title = model.Title;
            Description = model.Description;
            DueDate = model.DueDate;
            CreatedAt = model.CreatedAt;
            IsCompleted = model.IsCompleted;
            Priority = model.Priority;
        }

        public TaskDto ToDto()
        {
            return new TaskDto
            {
                Id = Id == Guid.Empty ? Guid.NewGuid() : Id,
                Title = Title,
                Description = Description,
                DueDate = DueDate,
                CreatedAt = CreatedAt == null ? DateTime.UtcNow : CreatedAt,
                IsCompleted = IsCompleted,
                Priority = Priority
            };
        }

        private void CloseDialog(bool result)
        {
            if (System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this) is Window window)
            {
                window.DialogResult = result;
                window.Close();
            }
        }

    }

}
