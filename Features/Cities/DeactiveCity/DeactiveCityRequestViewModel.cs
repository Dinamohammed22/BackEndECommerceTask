using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.DeactiveCity.Commands;

namespace KOG.ECommerce.Features.Cities.DeactiveCity
{
    public record DeactiveCityRequestViewModel(string ID);
    public class DeactiveCityRequestValidator : AbstractValidator<DeactiveCityRequestViewModel>
    {
        public DeactiveCityRequestValidator() { }
    }
    public class DeactiveCityRequestProfile : Profile
    {
        public DeactiveCityRequestProfile()
        {
            CreateMap<DeactiveCityRequestViewModel, DeactiveCityCommand>();
        }
    }
}
