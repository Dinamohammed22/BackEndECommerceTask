using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;

namespace KOG.ECommerce.Features.Products.GetNewProducts
{
    public record GetNewProductsResponseViewModel(string ID, string ProductName, double Price, string Path, int NumberOfPoints, 
        int MaximumQuantity, int MinimumQuantity, int ProductQuantity, string CompanyId, string CompanyName);
    public class GetNewProductsResponseProfile : Profile
    {
        public GetNewProductsResponseProfile()
        {
            CreateMap<ProductViewDTO, GetNewProductsResponseViewModel>();
        }
    }
}
