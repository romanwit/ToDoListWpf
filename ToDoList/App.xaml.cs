using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using ToDoList.Application.DependencyInjection;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Interfaces;
using ToDoList.Infrastructure.DependencyInjection;
using ToDoList.Infrastructure.Persistence;
using ToDoList.Infrastructure.Services;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private IHost _host;

        public App()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddApplication();

                    var connectionString = configuration.GetConnectionString("DefaultConnection");

                    services.AddInfrastructure(connectionString!);

                    services.AddSingleton<IPdfExporter, PdfExportService>();

                    services.AddSingleton<Presentation.ViewModels.MainWindowViewModel>();

                    services.AddSingleton<MainWindow>();
                })
                .Build();

            var scope = _host.Services.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<ITaskRepository>();
            var context = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
            context.Database.Migrate();
            var tasks = repo.GetAllAsync().Result.ToList();

           


        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }

}
