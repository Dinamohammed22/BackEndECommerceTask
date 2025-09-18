using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Governorates.BulkDeleteGovernorate.Orchestrator;

namespace KOG.ECommerce.Features.Governorates.BulkDeleteGovernorate
{
    public record BulkDeleteGovernorateRequestViewModel(List<string> Ids);
    public class BulkDeleteGovernorateRequestValidator:AbstractValidator<BulkDeleteGovernorateRequestViewModel>
    {
        public BulkDeleteGovernorateRequestValidator() { }
    }
    public class BulkDeleteGovernorateRequestProfile:Profile
    {
        public BulkDeleteGovernorateRequestProfile()
        {
            CreateMap<BulkDeleteGovernorateRequestViewModel, BulkDeleteGovernorateOrchestrator>();
        }
    }
}
