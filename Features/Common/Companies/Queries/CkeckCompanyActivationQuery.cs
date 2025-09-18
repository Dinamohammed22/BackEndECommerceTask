using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Common.Companies.Queries
{
    public record CkeckCompanyActivationQuery(string ID) : IRequestBase<bool>;
    public class CkeckCompanyActivationQueryHandler : RequestHandlerBase<Company, CkeckCompanyActivationQuery, bool>
    {
        public CkeckCompanyActivationQueryHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CkeckCompanyActivationQuery request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(c => c.ID == request.ID && c.IsActive == true);
            return RequestResult<bool>.Success(check);
        }
    }
}
