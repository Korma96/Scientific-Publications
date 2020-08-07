using AutoMapper;
using ScientificPublications.Common.Models;
using ScientificPublications.DataAccess.Model;
using ScientificPublications.Publication;
using ScientificPublications.WorkFlow;
using System.Linq;

namespace ScientificPublications.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, SessionDto>();
            CreateMap<RegisterDto, UserDto>();
            CreateMap<publication, PublicationDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.header.status))
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.title))
                .ForMember(dst => dst.Authors, opt => opt.MapFrom(src => src.authors.Select(a => a.username).ToList()));
            CreateMap<WorkFlowDto, workflow>();
        }
    }
}
