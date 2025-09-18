using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.RoleFeatures.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.RoleFeatures.GetFeaturesByRoleId
{
    public record GetFeaturesByRoleIdResponseViewModel(List<int> FeatureIds);
    
        public class GetFeaturesByRoleIdResponseProfile : Profile
        {
            public GetFeaturesByRoleIdResponseProfile()
            {
            CreateMap<List<int>, GetFeaturesByRoleIdResponseViewModel>()
                       .ConstructUsing(src => new GetFeaturesByRoleIdResponseViewModel(src));
        }
        }
    
}
