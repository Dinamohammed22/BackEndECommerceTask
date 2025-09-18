using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.ExportProductsReport
{
    public record ExportProductsReportRequestViewModel(string? ProductName, DateTime? From, DateTime? To);
    public class ExportProductsReportRequestValidator : AbstractValidator<ExportProductsReportRequestViewModel>
    {
        public ExportProductsReportRequestValidator() { }
    }
    public class ExportProductsReportRequestProfile:Profile
    {
        public ExportProductsReportRequestProfile()
        {
            CreateMap<ExportProductsReportRequestViewModel, ExportProductsReportQuery>();
        }
    }
}
