using AutoMapper;
using ToDoList.Application.DTOs;
using ToDoList.Domain.Entities;
using ToDoList.Presentation.Models;

namespace ToDoList.Application.Mapping
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskDto, TodoTask>().ReverseMap();
            CreateMap<TodoTask, TaskModel>().ReverseMap();
        }
    }
}
