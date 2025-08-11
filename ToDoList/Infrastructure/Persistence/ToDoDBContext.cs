using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure.Entitites;

namespace ToDoList.Infrastructure.Persistence
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
            : base(options)
        {
            

        }

        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
