using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Emails.SendEmailToClients.Commands;

namespace KOG.ECommerce.Features.Emails.SendEmailToClients
{
    public record SendEmailToClientsRequestViewModel(List<string> toEmails, string subject, string body);
    public class SendEmailToClientsRequestValidator : AbstractValidator<SendEmailToClientsRequestViewModel>
    {
        public SendEmailToClientsRequestValidator()
        {
            RuleFor(x => x.toEmails)
         .NotEmpty().WithMessage("The recipient email list is required.");

            RuleFor(x => x.subject)
                .NotEmpty().WithMessage("Email subject is required.");

            RuleFor(x => x.body)
                .NotEmpty().WithMessage("Email body is required.");
        }
    }

    public class SendEmailToClientsRequestEndPointRequestProfile : Profile
    {
        public SendEmailToClientsRequestEndPointRequestProfile()
        {
            CreateMap<SendEmailToClientsRequestViewModel, SendEmailToClientsCommand>();
            CreateMap<SendEmailToClientsCommand, CheckClientEmailQuery>();

        }
    }


}
