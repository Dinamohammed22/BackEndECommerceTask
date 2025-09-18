using AutoMapper;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Models.Medias;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
    public record ProductViewDTO
    {
        public string ID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string? Path { get; set; }
        public int NumberOfPoints { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
        public int ProductQuantity { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
    }

    public class ProductViewDTOProfile:Profile
    {
        public ProductViewDTOProfile() 
        {
            CreateMap<Product, ProductViewDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name)).ForMember(dest => dest.ProductQuantity, opt => opt.MapFrom(src => src.Quantity))
                               .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId)) .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src =>src.Company.Name));

            CreateMap<GetProductsByTypeQuery, GetFavoriteProductsQuery>();
            CreateMap<GetProductsByTypeQuery, GetNewProductsQuery>();
            CreateMap<GetProductsByTypeQuery, GetBestSellerProductsQuery>();

        }

    }
}
