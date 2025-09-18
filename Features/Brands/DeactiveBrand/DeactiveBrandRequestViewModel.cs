using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Brands.DeactiveBrand.Commands;

namespace KOG.ECommerce.Features.Brands.DeactiveBrand
{
    public record DeactiveBrandRequestViewModel(string ID);
    public class DeactiveBrandRequestValidator:AbstractValidator<DeactiveBrandRequestViewModel>
    {
        public DeactiveBrandRequestValidator() { }
    }
    public class DeactiveBrandRequestProfile : Profile
    {
        public DeactiveBrandRequestProfile()
        {
            CreateMap<DeactiveBrandRequestViewModel, DeactiveBrandCommand>();
        }
    }
}
