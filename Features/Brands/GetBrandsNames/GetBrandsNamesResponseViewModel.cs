using AutoMapper;
using KOG.ECommerce.Features.Common.Brands.DTOs;

namespace KOG.ECommerce.Features.Brands.GetBrandsNames
{
    public record GetBrandsNamesResponseViewModel(string ID, string Name, int NumberOfProducts);
    public class GetBrandsNamesResponseProfile : Profile
    {
        public GetBrandsNamesResponseProfile()
        {
            CreateMap<GetBrandsNamesDTO, GetBrandsNamesResponseViewModel>();
        }
    }
}
