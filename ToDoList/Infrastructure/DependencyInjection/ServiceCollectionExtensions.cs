using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Domain.Interfaces;
using ToDoList.Infrastructure.Persistence;
using ToDoList.Infrastructure.Repositories;

namespace ToDoList.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ToDoDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<ITaskRepository, SqliteTaskRepository>();

            return services;
        }
    }
}
