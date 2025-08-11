using ToDoList.Domain.Enums;

namespace ToDoList.Domain.Entities
{
    public class TodoTask
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public DateTime? DueDate { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public Priority Priority { get; private set; }

        public TodoTask(Guid id, string title, string? description = null, DateTime? dueDate = null, DateTime? createdAt = null,bool isCompleted = false, Priority priority = Priority.Normal)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));

            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            CreatedAt = createdAt;
            IsCompleted = isCompleted;
            Priority = priority;

        }
        public TodoTask(Guid id, string title, string? description = null, DateTime? dueDate = null, bool isCompleted = false, Priority priority = Priority.Normal)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));

            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            CreatedAt = DateTime.UtcNow;
            IsCompleted = isCompleted;
            Priority = priority;

        }
        public TodoTask(Guid id, string title, string? description = null, DateTime? dueDate = null, Priority priority = Priority.Normal)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));

            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            CreatedAt = DateTime.UtcNow;
            IsCompleted = false;
            Priority = priority;
          
        }

        public TodoTask(string title, string? description = null, DateTime? dueDate = null,Priority priority = Priority.Normal)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            DueDate = dueDate;
            CreatedAt = DateTime.UtcNow;
            IsCompleted = false;
            Priority = priority;
        }

        public void MarkCompleted() => IsCompleted = true;

        public void Update(string title, string? description, DateTime? dueDate, Priority priority, bool isCompleted)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));

            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            IsCompleted = isCompleted;
        }
    }

}
