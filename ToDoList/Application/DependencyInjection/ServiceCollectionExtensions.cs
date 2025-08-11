using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Mapping;

namespace ToDoList.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<TaskProfile>();
            });

            return services;
        }
    }
}
