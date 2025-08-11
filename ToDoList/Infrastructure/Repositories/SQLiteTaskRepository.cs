using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Interfaces;
using ToDoList.Infrastructure.Entitites;
using ToDoList.Infrastructure.Persistence;

namespace ToDoList.Infrastructure.Repositories
{
    public class SqliteTaskRepository : ITaskRepository
    {
        private readonly ToDoDbContext _context;

        public SqliteTaskRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TodoTask task)
        {
            var entity = MapToEntity(task);
            await _context.Tasks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Tasks.FindAsync(id);
            if (entity != null)
            {
                _context.Tasks.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TodoTask>> GetAllAsync()
        {
            var entities = await _context.Tasks.ToListAsync();
            return entities.Select(MapToDomain);
        }

        public async Task<TodoTask?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Tasks.FindAsync(id);
            return entity == null ? null : MapToDomain(entity);
        }

        public async Task UpdateAsync(TodoTask task)
        {
            var entity = await _context.Tasks.FindAsync(task.Id);
            if (entity != null)
            {
                entity.Title = task.Title;
                entity.Description = task.Description;
                entity.DueDate = task.DueDate;
                entity.IsCompleted = task.IsCompleted;
                entity.Priority = task.Priority;

                await _context.SaveChangesAsync();
            }
        }

        private static TaskEntity MapToEntity(TodoTask task) => new TaskEntity
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            CreatedAt = DateTime.UtcNow, 
            DueDate = task.DueDate
        };

        private static TodoTask MapToDomain(TaskEntity entity) => new TodoTask(
            entity.Id,
            entity.Title,
            entity.Description,
            entity.DueDate,
            entity.CreatedAt,
            entity.IsCompleted,
            entity.Priority
        );
    }
}
