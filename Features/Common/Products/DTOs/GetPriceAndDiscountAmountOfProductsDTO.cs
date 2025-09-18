using AutoMapper;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
    public record GetPriceAndDiscountAmountOfProductsDTO(string ProductId, int Quantity, double Price,double Amount);
    public class GetPriceAndDiscountAmountOfProductsProfileDTO:Profile
    {
        public GetPriceAndDiscountAmountOfProductsProfileDTO()
        {
            CreateMap<Product, GetPriceAndDiscountAmountOfProductsDTO>();
        }
    }
}
