using ToDoList.Application.DTOs;

namespace ToDoList.Application.Interfaces
{
    public interface IPdfExporter
    {
        void ExportTasksToPdf(IEnumerable<TaskDto> tasks, string filePath);
    }
}
