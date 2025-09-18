using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.BulkActivateCity.Orchestrator;

namespace KOG.ECommerce.Features.Cities.BulkActivateCity
{
    public record BulkActivateCityRequestViewModel(List<string> Ids);
    public class BulkActivateCityRequestValidator : AbstractValidator<BulkActivateCityRequestViewModel>
    {
        public BulkActivateCityRequestValidator() { }
    }
    public class BulkActivateCityRequestProfile : Profile
    {
        public BulkActivateCityRequestProfile()
        {
            CreateMap<BulkActivateCityRequestViewModel, BulkActivateCityOrchestrator>();
        }
    }
}
