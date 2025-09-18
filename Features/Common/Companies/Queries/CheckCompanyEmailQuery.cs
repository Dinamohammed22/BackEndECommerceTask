using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Companies;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Companies.Queries
{
    public record CheckCompanyEmailQuery(List<string> toEmails):IRequestBase<bool>;
    public class CheckCompanyEmailQueryHandler : RequestHandlerBase<Company, CheckCompanyEmailQuery, bool>
    {
        public CheckCompanyEmailQueryHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckCompanyEmailQuery request, CancellationToken cancellationToken)
        {
            var emailExists = await _repository.Get()
                .Where(company => request.toEmails.Contains(company.Email))
                .Select(company => company.Email)
                .ToListAsync();
            bool allEmailsExist = request.toEmails.All(email => emailExists.Contains(email));
            return RequestResult<bool>.Success(allEmailsExist);
        }
    }


}
