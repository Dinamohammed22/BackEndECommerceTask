using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Governorates.BulkActivateGovernorate.Orchestrator;

namespace KOG.ECommerce.Features.Governorates.BulkActivateGovernorate
{
    public record BulkActivateGovernorateRequestViewModel(List<string> Ids);
    public class BulkActivateGovernorateRequestValidator:AbstractValidator<BulkActivateGovernorateRequestViewModel>
    {
        public BulkActivateGovernorateRequestValidator() { }
    }
    public class BulkActivateGovernorateRequestProfile:Profile
    {
        public BulkActivateGovernorateRequestProfile()
        {
            CreateMap<BulkActivateGovernorateRequestViewModel, BulkActivateGovernorateOrchestrator>();
        }
    }
}
