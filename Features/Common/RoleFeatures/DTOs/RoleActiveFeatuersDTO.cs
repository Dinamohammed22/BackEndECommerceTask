using AutoMapper;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.RoleFeatures.DTOs
{
    public class RoleActiveFeatuersDTO
    {
        public int FeatureId { get; set; }
        public string FeatureName => ((Feature)FeatureId).ToString();
        public bool IsActiveToRole { get; set; }
    }
    public class RoleActiveFeatuersDTOProfile : Profile
    {
        public RoleActiveFeatuersDTOProfile()
        {
            CreateMap<Feature, RoleActiveFeatuersDTO>()
            .ForMember(dest => dest.FeatureId, opt => opt.MapFrom(src => src));
            CreateMap<RoleActiveFeatuersDTO, GetAllFeaturesListedDTO>();
        }
    }
}
