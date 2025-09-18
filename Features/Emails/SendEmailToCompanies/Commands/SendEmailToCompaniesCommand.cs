using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Common.Emails.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Emails;
using Org.BouncyCastle.Cms;

namespace KOG.ECommerce.Features.Emails.SendEmailToCompanies.Commands
{
    public record SendEmailToCompaniesCommand(List<string> toEmails, string subject, string body):IRequestBase<bool>;

    public class SendEmailToCompaniesCommandHandler : RequestHandlerBase<Email, SendEmailToCompaniesCommand, bool>
    {
        public SendEmailToCompaniesCommandHandler(RequestHandlerBaseParameters<Email> parameters) : base(parameters) { }
        public async override Task<RequestResult<bool>> Handle(SendEmailToCompaniesCommand request, CancellationToken cancellationToken)
        {
            var checkEmail = await _mediator.Send(request.MapOne<CheckCompanyEmailQuery>());
            if (checkEmail.Data)
            {
                EmailDTO emaildto = await EmailHelper.SendEmailAsync(request.toEmails, request.subject, request.body);
                if (emaildto != null)
                {
                    Email email = new Email { Subject = emaildto.Subject, Body = emaildto.Body, EmailAdresses = emaildto.EmailAdresses };
                    _repository.Add(email);
                    _repository.SaveChanges();
                }
                var result = RequestResult<bool>.Success(true);
                return await Task.FromResult(result);
            }
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        }
    }
}
