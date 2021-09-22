using AutoMapper;
using SchoolLogin.Dtos;
using SchoolLogin.Models;

namespace SchoolLogin.Profiles
{
    public class SchoolLoginProfiles : Profile
    {
        public SchoolLoginProfiles()
        {
            CreateMap<StudentRoster,SchoolLoginReadDto>();
            CreateMap<SchoolLoginCreateDto,StudentRoster>();
            CreateMap<SchoolLoginUpdateDto,StudentRoster>();

            //patch
            CreateMap<StudentRoster,SchoolLoginUpdateDto>();
            
        }
    }
}