using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Products.ProductsReport
{
    public class ProductsReportResponseViewModel
    {
        public string ID { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public Grade Grade { get; set; }
        public double? TotalPrice { get; set; }
        public double? TotalWeight { get; set; }
    }
    public class ProductsReportResponseProfile:Profile
    {
        public ProductsReportResponseProfile()
        {
            CreateMap<ProductsReportDTO, ProductsReportResponseViewModel>();
        }
    }
}
