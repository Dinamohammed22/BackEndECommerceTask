using AutoMapper;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.CartProducts.DTOs
{
    public record GetAllProductAtCartWithTotalPriceDTO(List<GetAllProductAtCartWithPriceDTO> GetAllProducts,double TotalPrice);
    public class GetAllProductAtCartWithTotalPriceDTOProfile : Profile
    {
        public GetAllProductAtCartWithTotalPriceDTOProfile()
        {
            CreateMap<Product, GetAllProductAtCartWithTotalPriceDTO>();
        }
    }
}
