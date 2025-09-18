using AutoMapper;
using KOG.ECommerce.Models.CartProducts;

namespace KOG.ECommerce.Features.Common.CartProducts.DTOs
{
    public record GetCartProductsNameAndPathDTO(string ProductName, string Path);
    public class GetCartProductsNameAndPathDTOProfile : Profile
    {
        public GetCartProductsNameAndPathDTOProfile()
        {
            CreateMap<CartProduct, GetCartProductsNameAndPathDTO>();

        }
    }
}
