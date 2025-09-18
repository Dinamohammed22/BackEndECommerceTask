using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Users.BulkDeActivateUser.Orchestrator;

namespace KOG.ECommerce.Features.Users.BulkDeActivateUser
{
    public record BulkDeActivateUserRequestViewModel(List<string> IDs);
    public class BulkDeActivateUserRequestValidator : AbstractValidator<BulkDeActivateUserRequestViewModel>
    {
        public BulkDeActivateUserRequestValidator() { }
    }
    public class BulkDeActivateUserRequestProfile : Profile
    {
        public BulkDeActivateUserRequestProfile()
        {
            CreateMap<BulkDeActivateUserRequestViewModel, BulkDeActivateUserOrchestrator>();
        }
    }
}
