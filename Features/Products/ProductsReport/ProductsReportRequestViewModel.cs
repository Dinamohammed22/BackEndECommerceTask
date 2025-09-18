using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Products.ProductsReport
{
    public record ProductsReportRequestViewModel(
                                      string? ProductName,
                                      DateTime? From,
                                      DateTime? To,
                                      int pageIndex = 1, int pageSize = 100);
    public class ProductsReportRequestValidator:AbstractValidator<ProductsReportRequestViewModel>
    {
        public ProductsReportRequestValidator()
        {

        }
    }
    public class ProductsReportRequestProfile:Profile
    {
        public ProductsReportRequestProfile()
        {
            CreateMap<ProductsReportRequestViewModel, ProductsReportQuery>();
        }
    }
}
