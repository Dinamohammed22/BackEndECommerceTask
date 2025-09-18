using AutoMapper;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
    public record ExportProductsReportDTO(byte[] FileContent, string FileName, string ContentType);
    public class ExportProductsReportDTOProfile:Profile
    {
        public ExportProductsReportDTOProfile()
        {
            CreateMap<Product, ProductsReportDTO>()
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
