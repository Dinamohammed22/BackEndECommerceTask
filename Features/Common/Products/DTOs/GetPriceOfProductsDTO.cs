using AutoMapper;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
    public record GetPriceOfProductsDTO(string ProductId, int Quantity,double Price ,double Liter, int Point);
    public class GetPriceOfProductsDTOProfile : Profile
    {
        public GetPriceOfProductsDTOProfile()
        {
            CreateMap<Product, GetPriceOfProductsDTO>();
        }
    }
}
