using AutoMapper;
using DTOs.ResponseDTO;
using Repository.Models;

namespace Task_managment.AutoMapper
{
    public class MappingProfiles : Profile
    {
        protected MappingProfiles()
        {
            CreateMap<Tasks, TaskDTO>().ReverseMap();
            
        }
    }
}
