using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Products.GetBestSellerProducts;

namespace KOG.ECommerce.Features.Products.GetProductsByType
{
    public record GetProductsByTypeResponseViewModel(string ID, string ProductName, double Price, string Path, int ProductQuantity,
        int MaximumQuantity, int MinimumQuantity, string CompanyId, string CompanyName);
    public class GetProductsByTypeResponseProfile : Profile
    {
        public GetProductsByTypeResponseProfile()
        {
            CreateMap<ProductViewDTO, GetProductsByTypeResponseViewModel>();
        }
    }
}
