using AutoMapper;
using KOG.ECommerce.Features.Common.RoleFeatures.Queries;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.RoleFeatures.DTOs
{
    public class GetAllFeaturesListedDTO
    {
        public string SectionName { get; set; }
        public List<RoleActiveFeatuersDTO> Features{ get; set; }
    }
    public class GetAllFeaturesListedDTOProfile : Profile
    {
        public GetAllFeaturesListedDTOProfile()
        {
            CreateMap<Feature, GetAllFeaturesListedDTO>();
            CreateMap<GetAllFeaturesListedDTO, RoleActiveFeatuersDTO>();
        }
    }
}
