using AutoMapper;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
    public record GetProductCountByBrandIdDTO (int NumberOfProducts);
    public class GetProductCountByBrandIdDTOProfile : Profile
    {
        public GetProductCountByBrandIdDTOProfile()
        {
            CreateMap<Product, GetProductCountByBrandIdDTO>();

        }
    }
}
