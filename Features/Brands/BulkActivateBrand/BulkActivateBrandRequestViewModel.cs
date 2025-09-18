using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Brands.BulkDeleteBrand.Orchisterator;
using KOG.ECommerce.Features.Brands.BulkDeleteBrand;
using KOG.ECommerce.Features.Brands.BulkActivateBrand.Orchisterator;

namespace KOG.ECommerce.Features.Brands.BulkActivateBrand
{
    public record BulkActivateBrandRequestViewModel(List<string> Ids);

    public class BulkActivateBrandRequestValidator : AbstractValidator<BulkActivateBrandRequestViewModel>
    {
        public BulkActivateBrandRequestValidator()
        {
        }
    }
    public class BulkAcvtivateBrandRequestProfile : Profile
    {
        public BulkAcvtivateBrandRequestProfile()
        {
            CreateMap<BulkActivateBrandRequestViewModel, BulkActivateBrandOrchisterator>();
        }
    }
}
