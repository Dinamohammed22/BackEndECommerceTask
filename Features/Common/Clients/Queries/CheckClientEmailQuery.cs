using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Clients;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public record CheckClientEmailQuery(List<string> Emails) : IRequestBase<bool>;
    public class CheckClientEmailQueryHandler : RequestHandlerBase<Client, CheckClientEmailQuery, bool>
    {
        public CheckClientEmailQueryHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckClientEmailQuery request, CancellationToken cancellationToken)
        {
            if (request.Emails == null || request.Emails.Count == 0)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            var loweredRequestEmails = request.Emails
                .Where(e => !string.IsNullOrWhiteSpace(e))
                .Select(e => e.ToLower())
                .ToList();

            var emailExists = await _repository.Get()
                .Where(c => c.Email != null && loweredRequestEmails.Contains(c.Email.ToLower()))
                .Select(c => c.Email.ToLower())
                .ToListAsync(cancellationToken);

            bool allEmailsExist = loweredRequestEmails.All(email => emailExists.Contains(email));

            return RequestResult<bool>.Success(allEmailsExist);
        }

    }

}
