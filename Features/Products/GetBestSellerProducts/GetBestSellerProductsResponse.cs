using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;

namespace KOG.ECommerce.Features.Products.GetBestSellerProducts
{
    public record GetBestSellerProductsResponse(string ID, string ProductName, double Price, string Path, int NumberOfPoints, 
        int MaximumQuantity, int MinimumQuantity, int ProductQuantity, string CompanyId, string CompanyName);
    public class GetBestSellerProductsResponseProfile : Profile
    {
        public GetBestSellerProductsResponseProfile()
        {
            CreateMap<ProductViewDTO, GetBestSellerProductsResponse>();
        }
    }
}
