using AutoMapper;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ModuleFeatures;

namespace KOG.ECommerce.Features.Common.ModuleFeatures.DTOs
{
    public record GetFeaturesByModuleIdDTO(Feature Features)
    {
        public string FeatureTypeName => Enum.GetName(typeof(Feature), Features) ?? string.Empty;
    }
    public class GetFeaturesByModuleIdProfile : Profile
    {
        public GetFeaturesByModuleIdProfile()
        {
            CreateMap<ModuleFeature, GetFeaturesByModuleIdDTO>();
        }
    }
}
