using AutoMapper;
using TaskManager.Api.Dtos;
using TaskManager.Api.Entities;

namespace TaskManager.Api.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ToDoTask, TaskAllDto>().ReverseMap();
        }
    }
}
