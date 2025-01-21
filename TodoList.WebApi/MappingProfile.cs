using AutoMapper;
using Models.Entities;
using ToDoLIstAPi.DTO.Tasks;
using ToDoLIstAPi.DTO.User;

namespace ToDoLIstAPi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tasks, TaskOutputDto>().ReverseMap();
        CreateMap<Tasks, TaskAddForUserDto>().ReverseMap();
        
        CreateMap<User, UserDtoOutput>().ReverseMap();
        CreateMap<User, Userinput>().ReverseMap();
        CreateMap<User, UserDtoManipulation>().ReverseMap();
        
    }
    
}
