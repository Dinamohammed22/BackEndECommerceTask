using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Products.ExportProductsToExcel
{
    public record ExportProductsToExcelRequestViewModel(string? ProductName, string? CategoryId, string? SubcategoryId, bool? IsActive, string? CompanyId, DateTime? From,
                                      DateTime? To);
    public class ExportProductsToExcelRequestValidator : AbstractValidator<ExportProductsToExcelRequestViewModel>
    {
        public ExportProductsToExcelRequestValidator()
        {
        }
    }
    public class ExportProductsToExcelRequestProfile : Profile
    {
        public ExportProductsToExcelRequestProfile()
        {
            CreateMap<ExportProductsToExcelRequestViewModel, ExportProductsToExcelQuery>();
        }
    }
}
