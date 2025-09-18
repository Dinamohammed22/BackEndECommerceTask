using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.BulkDeactivateCity.Orchestrator;

namespace KOG.ECommerce.Features.Cities.BulkDeactivateCity
{
    public record BulkDeactivateCityRequestViewModel(List<string> Ids);
    public class BulkDeactivateCityRequestValidator : AbstractValidator<BulkDeactivateCityRequestViewModel>
    {
        public BulkDeactivateCityRequestValidator() { }
    }
    public class BulkDeactivateCityRequestProfile : Profile
    {
        public BulkDeactivateCityRequestProfile()
        {
            CreateMap<BulkDeactivateCityRequestViewModel, BulkDeactivateCityOrchestrator>();
        }
    }
}
