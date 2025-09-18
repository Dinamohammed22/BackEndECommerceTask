using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Brands.BulkActivateBrand.Orchisterator;
using KOG.ECommerce.Features.Brands.BulkActivateBrand;
using KOG.ECommerce.Features.Brands.BulkDeActivateBrand.Orchisterator;

namespace KOG.ECommerce.Features.Brands.BulkDeActivateBrand
{
    public record BulkDeActivateBrandRequestViewModel(List<string> Ids);

    public class BulkDeActivateBrandRequestValidator : AbstractValidator<BulkDeActivateBrandRequestViewModel>
    {
        public BulkDeActivateBrandRequestValidator()
        {
        }
    }
    public class BulkDeAcvtivateBrandRequestProfile : Profile
    {
        public BulkDeAcvtivateBrandRequestProfile()
        {
            CreateMap<BulkDeActivateBrandRequestViewModel, BulkDeActivateBrandOrchisterator>();
        }
    }
}
