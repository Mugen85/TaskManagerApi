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

            // Mapping da Entità a DTO di risposta
            CreateMap<ToDoTask, TaskResponseDto>();

            // Mapping da DTO di creazione a Entità
            CreateMap<CreateTaskRequestDto, ToDoTask>();

            // Mapping da DTO di aggiornamento a Entità
            CreateMap<UpdateTaskRequestDto, ToDoTask>();
        }
    }
}
