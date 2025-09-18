using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Users.BulkActivateUser.Orchestrator;

namespace KOG.ECommerce.Features.Users.BulkActivateUser
{
    public record BulkActivateUserRequestViewModel(List<string> IDs);
    public class BulkActivateUserRequestValidator : AbstractValidator<BulkActivateUserRequestViewModel>
    {
        public BulkActivateUserRequestValidator() { }
    }
    public class BulkActivateUserRequestProfile : Profile
    {
        public BulkActivateUserRequestProfile()
        {
            CreateMap<BulkActivateUserRequestViewModel, BulkActivateUserOrchestrator>();
        }
    }
}
