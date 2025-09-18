using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Emails.SendEmailToCompanies.Commands;

namespace KOG.ECommerce.Features.Emails.SendEmailToCompanies
{
    public record SendEmailToCompaniesRequestViewModel(List<string> toEmails, string subject, string body);
    public class SendEmailToCompaniesRequestValidator : AbstractValidator<SendEmailToCompaniesRequestViewModel>
    {
        public SendEmailToCompaniesRequestValidator()
        {
            RuleFor(x => x.toEmails)
     .NotEmpty().WithMessage("The recipient email list is required.");

            RuleFor(x => x.subject)
                .NotEmpty().WithMessage("Email subject is required.");

            RuleFor(x => x.body)
                .NotEmpty().WithMessage("Email body is required.");
        }
    }

    public class SendEmailToCompaniesRequestEndPointRequestProfile : Profile
    {
        public SendEmailToCompaniesRequestEndPointRequestProfile()
        {
            CreateMap<SendEmailToCompaniesRequestViewModel, SendEmailToCompaniesCommand>();
            CreateMap<SendEmailToCompaniesCommand, CheckCompanyEmailQuery>();

        }
    }

}
