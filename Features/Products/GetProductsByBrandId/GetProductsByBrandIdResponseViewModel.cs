using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;

namespace KOG.ECommerce.Features.Products.GetProductsByBrandId
{
    public record GetProductsByBrandIdResponseViewModel(string ID, string ProductName ,double Price,string? Path, string CompanyId, string CompanyName);
    public class GetProductsByBrandIdResponseProfile : Profile
    {
        public GetProductsByBrandIdResponseProfile()
        {
            CreateMap<ProductViewDTO, GetProductsByBrandIdResponseViewModel>();
        }
    }
}
