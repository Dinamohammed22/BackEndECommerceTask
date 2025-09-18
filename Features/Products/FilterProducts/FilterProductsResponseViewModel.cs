using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;

namespace KOG.ECommerce.Features.Products.FilterProducts
{
    public record FilterProductsResponseViewModel(string ID, string ProductName, double Price ,string? Path,int NumberOfPoints,int MaximumQuantity,
        int MinimumQuantity, int ProductQuantity, string CompanyId, string CompanyName);
    public class FilterProductsResponseProfile : Profile
    {
        public FilterProductsResponseProfile()
        {
            CreateMap<ProductViewDTO, FilterProductsResponseViewModel>();
        }
    }
}
