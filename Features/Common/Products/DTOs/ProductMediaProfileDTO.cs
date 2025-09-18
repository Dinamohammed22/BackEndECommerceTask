using AutoMapper;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
    public record ProductMediaProfileDTO
    {
        public string ID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CategoryId { get; init; }
        public string CategoryName { get; init; }
        public List<string> Tags { get; init; } = new List<string>();
        public string Model { get; init; }
        public double Price { get; init; }
        public double Tax { get; init; }
        public string BrandId { get; init; }
        public string BrandName { get; init; }
        public int MinimumQuantity { get; init; }
        public int MaximumQuantity { get; init; }
        public double Length { get; init; }
        public string SubCategoryId { get; set; }  
        public string SubCategoryName { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public double Width { get; init; }
        public double Height { get; init; }
        public double Liter { get; init; }
        public DateTime AvailableDate { get; init; }
        public string SpecificationMetrix { get; init; }
        public string Data { get; init; }
        public bool FeaturedProduct { get; init; }
        public int Quantity { get; init; }
        public int NumberOfPoints { get; set; }
        public bool IsActivePoint { get; init; }
        public bool IsActive {  get; init; }
        public Grade Grade { get; set; }

    }

    public class ProductMediaProfile : Profile
    {
        public ProductMediaProfile()
        {
            CreateMap<Product, ProductMediaProfileDTO>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags ?? new List<string>()))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId ?? string.Empty))
                .ForMember(dest => dest.SubCategoryId, opt => opt.Ignore())
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : string.Empty))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : string.Empty))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
                .ForMember(dest => dest.SubCategoryName, opt => opt.Ignore()) 
                .AfterMap((src, dest) =>
                {
                    dest.SubCategoryId = src.Category?.Subcategories?.FirstOrDefault()?.ID ?? string.Empty;
                    dest.SubCategoryName = src.Category?.Subcategories?.FirstOrDefault()?.Name ?? string.Empty;
                });

            CreateMap<ProductMediaProfileDTO, ProductWithMediaDTO>();
        }
    }


}
