using AutoMapper;
using KOG.ECommerce.Features.Common.ModuleFeatures.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ModuleFeatures.GetFeaturesByModuleId
{
    public record GetFeaturesByModuleIdResponseViewModel(string FeatureTypeName);

    public class GetFeaturesByModuleIdResponseProfile : Profile
    {
        public GetFeaturesByModuleIdResponseProfile()
        {
           
            CreateMap<GetFeaturesByModuleIdDTO, GetFeaturesByModuleIdResponseViewModel>()
                .ForMember(dest => dest.FeatureTypeName, opt => opt.MapFrom(src => Enum.GetName(typeof(Feature), src.Features) ?? string.Empty));
        }
    }
}
