using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Features.Governorates.DeactiveGovernorate.Orchestrators;
using KOG.ECommerce.Features.Governorates.DeactiveGovernorate.Commands;

namespace KOG.ECommerce.Features.Governorates.DeactiveGovernorate
{
    public record DeactiveGovernorateRequestViewModel(string ID);
    public class DeactiveGovernorateRequestValidator : AbstractValidator<DeactiveGovernorateRequestViewModel>
    {
        public DeactiveGovernorateRequestValidator()
        {
        }
    }
    public class DeactiveGovernorateRequestProfile : Profile
    {
        public DeactiveGovernorateRequestProfile()
        {
            CreateMap<DeactiveGovernorateRequestViewModel, DeactiveGovernorateOrchestrator>();
            CreateMap<DeactiveGovernorateOrchestrator, DeactiveGovernorateCommand>();
            CreateMap<DeactiveGovernorateOrchestrator, GetCitiesIdsByGovernorateIDQuery>();
        }
    }
}
