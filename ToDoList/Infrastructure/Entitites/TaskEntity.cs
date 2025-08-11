using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.Enums;

namespace ToDoList.Infrastructure.Entitites
{
    [Table("Tasks")]
    public class TaskEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CreatedAt { get; set; }

        public bool IsCompleted { get; set; }

        public Priority Priority { get; set; }

      
    }
}
