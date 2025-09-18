using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.NotificationMessages.SendNotification.Orchestrator;

namespace KOG.ECommerce.Features.NotificationMessages.SendNotification
{
    public record SendNotificationRequestViewModel(List<string> UserId, string Title, string Body);
    public class SendNotificationRequestValidator : AbstractValidator<SendNotificationRequestViewModel>
    {
        public SendNotificationRequestValidator()
        {
            RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("At least one user ID is required.")
            .Must(list => list.All(id => !string.IsNullOrWhiteSpace(id)))
            .WithMessage("All user IDs must be non-empty and not whitespace.");

            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Body is required.")
                .MaximumLength(500).WithMessage("Body cannot exceed 500 characters.");

        }

    }

    public class SendNotificationRequestProfile : Profile
    {
        public SendNotificationRequestProfile()
        {
            CreateMap<SendNotificationRequestViewModel, SendNotificationOrchestrator>();
        }
    }
}
