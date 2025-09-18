using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;

namespace KOG.ECommerce.Features.Products.GetFavoriteProducts
{
    public record GetFavoriteProductsResponseViewModel(string ID, string ProductName, double Price, string? Path, int NumberOfPoints, int MaximumQuantity, 
        int MinimumQuantity, int ProductQuantity, string CompanyId, string CompanyName);
    public class GetFavoriteProductsResponseProfile:Profile
    {
        public GetFavoriteProductsResponseProfile()
        {
            CreateMap<ProductViewDTO, GetFavoriteProductsResponseViewModel>();
        }
    }


}
