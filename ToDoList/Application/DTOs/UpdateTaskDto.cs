using ToDoList.Domain.Enums;

namespace ToDoList.Application.DTOs
{
    public class UpdateTaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; }
 
    }
}
