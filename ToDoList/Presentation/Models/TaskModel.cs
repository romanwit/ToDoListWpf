using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;

namespace ToDoList.Presentation.Models
{
    public class TaskModel
    {

        public TaskModel(TodoTask entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Description = entity.Description;
            DueDate = entity.DueDate;
            CreatedAt = entity.CreatedAt;
            IsCompleted = entity.IsCompleted;
            Priority = entity.Priority;
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; }

        public string DueDateFormatted => DueDate?.ToString("dd MMM yyyy") ?? "-";
        public string CreatedAtFormatted => CreatedAt?.ToString("dd MMM yyyy") ?? "-";

        public string PriorityText => Priority.ToString();
    }
}
