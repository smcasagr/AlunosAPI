using AlunosAPI.Models;
using AutoMapper;

namespace AlunosAPI.DTOs.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Aluno, AlunoDTO>().ReverseMap();
        }
    }
}
