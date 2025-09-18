using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.ActiveCity.Commands;
using KOG.ECommerce.Features.Cities.ActiveCity.Orchestrators;

namespace KOG.ECommerce.Features.Cities.ActiveCity
{
    public record ActiveCityRequestViewModel(string ID);
    public class ActiveCityRequestValidator : AbstractValidator<ActiveCityRequestViewModel>
    {
        public ActiveCityRequestValidator() { }
    }
    public class ActiveCityRequestProfile : Profile
    {
        public ActiveCityRequestProfile()
        {
            CreateMap<ActiveCityRequestViewModel, ActiveCityOrchestrator>();
            CreateMap<ActiveCityOrchestrator, ActiveCityCommand>();
        }
    }
}
