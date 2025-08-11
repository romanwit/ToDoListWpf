using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Mapping;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Domain.Interfaces;
using ToDoList.Presentation.ViewModels;


namespace ToDoList.Tests
{
    public class MainWindowViewModelTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock = new();
        private readonly Mock<IPdfExporter> _pdfExporterMock = new();
        private readonly IMapper _mapper;

        public MainWindowViewModelTests()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var config = new MapperConfiguration(cfg => cfg.AddProfile<TaskProfile>(), loggerFactory);
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task LoadTasks_ShouldPopulateTasksCollection()
        {
            var tasks = new List<TodoTask>
            {
                new(Guid.NewGuid(), "Test task",null,null,Priority.Normal)
            };
            _taskRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(tasks);

            var vm = new MainWindowViewModel(_mapper, _taskRepositoryMock.Object, _pdfExporterMock.Object);

            await Task.Delay(100);

            Assert.Single(vm.Tasks);
            Assert.Equal("Test task", vm.Tasks[0].Title);
        }
    }
}