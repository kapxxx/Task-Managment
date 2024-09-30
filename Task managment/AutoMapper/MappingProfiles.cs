using AutoMapper;
using DTOs.ResponseDTO;
using Repository.Models;

namespace Task_managment.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Tasks, TaskDTO>().ReverseMap();
            
        }
    }
}
