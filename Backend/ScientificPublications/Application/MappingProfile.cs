using AutoMapper;
using ScientificPublications.Common.Models;

namespace ScientificPublications.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, SessionDto>();
            CreateMap<RegisterDto, UserDto>();
        }
    }
}
