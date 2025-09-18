using AutoMapper;
using KOG.ECommerce.Features.Common.Brands.DTOs;
using KOG.ECommerce.Features.Common.Medias.DTOs;

namespace KOG.ECommerce.Features.Brands.GetBrandByID
{
    public record GetBrandByIDResponseViewModel(string ID, string Name, List<string> Tags, bool IsActive, List<MediaDTO>? Media );

    public class GetBrandByIDResponseProfile:Profile
    {
        public GetBrandByIDResponseProfile()
        {
            CreateMap<BrandProfileDTO, GetBrandByIDResponseViewModel>();
        }
    }
}
