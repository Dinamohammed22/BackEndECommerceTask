using AutoMapper;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
    public class SearchProductProfileDTO
    {
        public string ID { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public bool IsActivePoint { get; set; }
        public int NumberOfPoints { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public Grade Grade { get; set; }
        public double? TotalPrice { get; set; }
        public double? TotalWeight { get; set; }
    }

    public class SearchProductProfile : Profile
    {
        public SearchProductProfile()
        {
            CreateMap<Product, SearchProductProfileDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SubcategoryName, opt => opt.MapFrom(src =>
                    src.Category.ParentCategory != null
                        ? src.Category.Name 
                        : null))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src =>
                    src.Company.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                    src.Category.ParentCategory != null
                        ? src.Category.ParentCategory.Name 
                        : src.Category.Name));
        }
    }


}
