using AutoMapper;
using KOG.ECommerce.Features.Common.RoleFeatures.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.RoleFeatures.GetAllFeaturesListed
{
    public record GetAllFeaturesListedResponseViewModel(string SectionName , List<RoleActiveFeatuersDTO> Features);
    public class GetAllFeaturesListedResponserofile : Profile
    {
        public GetAllFeaturesListedResponserofile()
        {
            CreateMap<GetAllFeaturesListedDTO, GetAllFeaturesListedResponseViewModel>();
        }
    }
}
