using AutoMapper;
using ScientificPublications.Common.Models;

namespace ScientificPublications.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DataAccess.Model.User, UserDto>();
        }
    }
}
