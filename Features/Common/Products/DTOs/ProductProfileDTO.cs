using AutoMapper;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.Categories;
using KOG.ECommerce.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
    public class ProductProfileDTO
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public List<string> Tags { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }
        public string BrandName { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Liter { get; set; }
        public DateTime AvailableDate { get; set; }
        public int NumberOfPoints { get; set; }
    }
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductProfileDTO>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src =>src.Name))
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src =>src.Brand.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>src.Category.Name));

        }
    }
    
}
