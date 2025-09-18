using AutoMapper;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
    public record ProductIDandQuantityDTO(string ProductId, int Quantity);
    public class ProductIDandQuantityDTOProfile : Profile
    {
        public ProductIDandQuantityDTOProfile()
        {
            CreateMap<Product, ProductIDandQuantityDTO>();
        }
    }
}
