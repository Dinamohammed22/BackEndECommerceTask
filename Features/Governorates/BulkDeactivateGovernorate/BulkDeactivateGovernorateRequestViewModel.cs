using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Governorates.BulkDeactivateGovernorate.Orchestrator;

namespace KOG.ECommerce.Features.Governorates.BulkDeactivateGovernorate
{
    public record BulkDeactivateGovernorateRequestViewModel(List<string> Ids);
    public class BulkDeactivateGovernorateRequestValidator:AbstractValidator<BulkDeactivateGovernorateRequestViewModel>
    {
        public BulkDeactivateGovernorateRequestValidator() { }
    }
    public class BulkDeactivateGovernorateRequestProfile:Profile
    {
        public BulkDeactivateGovernorateRequestProfile()
        {
            CreateMap<BulkDeactivateGovernorateRequestViewModel, BulkDeactivateGovernorateOrchestrator>();
        }
    }
}
