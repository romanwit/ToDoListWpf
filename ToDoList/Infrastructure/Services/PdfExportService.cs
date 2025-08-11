using ToDoList.Application.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using ToDoList.Application.DTOs;

namespace ToDoList.Infrastructure.Services
{

    public class PdfExportService : IPdfExporter
    {
        public void ExportTasksToPdf(IEnumerable<TaskDto> tasks, string filePath)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("Task Manager Export")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Title").Bold();
                                header.Cell().Text("Due Date").Bold();
                                header.Cell().Text("Priority").Bold();
                                header.Cell().Text("Completed").Bold();
                            });

                            foreach (var task in tasks)
                            {
                                table.Cell().Text(task.Title);
                                table.Cell().Text(task.DueDate?.ToString("yyyy-MM-dd") ?? "-");
                                table.Cell().Text(task.Priority.ToString());
                                table.Cell().Text(task.IsCompleted ? "Yes" : "No");
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(txt =>
                        {
                            txt.Span("Page ");
                            txt.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdf(filePath);
        }
    }
}
