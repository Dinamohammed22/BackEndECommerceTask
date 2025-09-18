using AutoMapper;
using KOG.ECommerce.Features.Common.Medias.DTOs;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.Products.GetProductById;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
    public class ProductWithMediaDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<string> Tags { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }
        public string BrandId { get; set; }
        public string BrandName { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
        public double Length { get; set; }
        public string? SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Liter { get; set; }
        public DateTime AvailableDate { get; set; }
        public string SpecificationMetrix { get; set; }
        public string Data { get; set; }
        public bool FeaturedProduct { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public bool IsActivePoint { get; set; }
        public int NumberOfPoints { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public Grade Grade { get; set; }
        public List<MediaDTO>? Media { get; set; }
    }


    public class ProductWithMediaDTOProfile:Profile
    {
        public ProductWithMediaDTOProfile()
        {
            CreateMap<ProductMediaProfileDTO, ProductWithMediaDTO>()
            .ForMember(dest => dest.Media, opt => opt.Ignore());

            CreateMap<ProductWithMediaDTO, GetProductByIDResponseViewModel>();
            CreateMap<GetProductByIDWithMediaQuery, ProductWithMediaDTO>();

        }
    }
}
