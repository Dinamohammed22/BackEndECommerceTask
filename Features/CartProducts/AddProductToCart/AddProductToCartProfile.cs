using AutoMapper;
using KOG.ECommerce.Features.CartProducts.AddProductToCart.Commands;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.CartProducts.AddProductToCart
{
    public class AddProductToCartProfile:Profile
    {
        public AddProductToCartProfile()
        {
            CreateMap<AddProductToCartCommand, CheckProductById>();
        }
    }
}
