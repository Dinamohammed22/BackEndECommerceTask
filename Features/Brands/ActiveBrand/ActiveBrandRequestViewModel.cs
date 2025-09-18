using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Brands.ActiveBrand.Commands;

namespace KOG.ECommerce.Features.Brands.ActiveBrand
{
    public record ActiveBrandRequestViewModel(string ID);
    public class ActiveBrandRequestValidator : AbstractValidator<ActiveBrandRequestViewModel>
    {
        public ActiveBrandRequestValidator() { }
    }
    public class ActiveBrandRequestProfile : Profile
    {
        public ActiveBrandRequestProfile()
        {
            CreateMap<ActiveBrandRequestViewModel, ActiveBrandCommand>();
        }
    }
}
