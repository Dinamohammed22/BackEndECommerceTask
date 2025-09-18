using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Common.Emails.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Emails;

namespace KOG.ECommerce.Features.Emails.SendEmailToClients.Commands
{
    public record SendEmailToClientsCommand(List<string> toEmails, string subject, string body) : IRequestBase<bool>;
    public class SendEmailToClientsCommandHandler : RequestHandlerBase<Email, SendEmailToClientsCommand, bool>
    {
        public SendEmailToClientsCommandHandler(RequestHandlerBaseParameters<Email> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(SendEmailToClientsCommand request, CancellationToken cancellationToken)
        {
            
                EmailDTO emaildto = await EmailHelper.SendEmailAsync(request.toEmails, request.subject, request.body);
                if (emaildto != null)
                {
                    Email email = new Email { Subject = emaildto.Subject, Body = emaildto.Body, EmailAdresses = emaildto.EmailAdresses };
                    _repository.Add(email);  
                }

                var result = RequestResult<bool>.Success(true);
                return await Task.FromResult(result);
        }

    }

}
