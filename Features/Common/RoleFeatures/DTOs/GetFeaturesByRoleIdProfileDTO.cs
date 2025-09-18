using AutoMapper;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.RoleFeatures.DTOs
{
    public class GetFeaturesByRoleIdProfileDTO
    {
       public int FeatureId { get; set; }
    }
    public class GetFeaturesByRoleIdProfile : Profile
    {
        public GetFeaturesByRoleIdProfile()
        {
            CreateMap<Feature, GetFeaturesByRoleIdProfileDTO>()
            .ForMember(dest => dest.FeatureId, opt => opt.MapFrom(src => src));
            CreateMap<GetFeaturesByRoleIdProfileDTO, GetAllFeaturesListedDTO>();
        }
    }
}
