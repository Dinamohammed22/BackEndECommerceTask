using AutoMapper;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
    public record GetDiscountAmountOfProductsDTO(string ProductId, double Amount);
    public class GetDiscountAmountOfProductsProfileDTO:Profile
    {
        public GetDiscountAmountOfProductsProfileDTO()
        {
            CreateMap<Product, GetDiscountAmountOfProductsDTO>();
        }
    }
}
