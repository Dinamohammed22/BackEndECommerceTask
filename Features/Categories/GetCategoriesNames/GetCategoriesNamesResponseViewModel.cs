using AutoMapper;
using KOG.ECommerce.Features.Common.Categories.DTOs;

namespace KOG.ECommerce.Features.Categories.GetCategoriesNames
{
    public record GetCategoriesNamesResponseViewModel(string ID, string Name, int ProductsNumber);
    public class GetCategoriesNamesResponseProfile:Profile
    {
        public GetCategoriesNamesResponseProfile()
        {
            CreateMap<GetCategoriesNamesDTO, GetCategoriesNamesResponseViewModel>();
        }
    }
}
