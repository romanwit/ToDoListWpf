using ToDoList.Domain.Enums;

namespace ToDoList.Application.DTOs
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Priority Priority { get; set; } = Priority.Normal;
    }
}
