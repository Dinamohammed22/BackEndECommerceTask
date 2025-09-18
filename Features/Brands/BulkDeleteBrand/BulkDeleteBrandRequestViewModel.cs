using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Brands.DeleteBrand.Commands;
using KOG.ECommerce.Features.Brands.DeleteBrand;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.Brands.BulkDeleteBrand.Orchisterator;

namespace KOG.ECommerce.Features.Brands.BulkDeleteBrand
{
    public record BulkDeleteBrandRequestViewModel(List<string> Ids);

    public class BulkDeleteBrandRequestValidator : AbstractValidator<BulkDeleteBrandRequestViewModel>
    {
        public BulkDeleteBrandRequestValidator()
        {
        }
    }
    public class BulkDeleteBrandRequestProfile : Profile
    {
        public BulkDeleteBrandRequestProfile()
        {
            CreateMap<BulkDeleteBrandRequestViewModel, BulkDeleteBrandOrchisterator>();
        }
    }
}
