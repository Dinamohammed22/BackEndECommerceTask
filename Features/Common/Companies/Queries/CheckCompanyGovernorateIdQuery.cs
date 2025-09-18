using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Common.Companies.Queries
{
    public record CheckCompanyGovernorateIdQuery(string ID) : IRequestBase<bool>;
    public class CheckCompanyGovernorateIdQueryHandler : RequestHandlerBase<Company, CheckCompanyGovernorateIdQuery, bool>
    {
        public CheckCompanyGovernorateIdQueryHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckCompanyGovernorateIdQuery request, CancellationToken cancellationToken)
        {
           var ISExist=await _repository.AnyAsync(c=>c.GovernorateId==request.ID);
            return RequestResult<bool>.Success(ISExist);
        }
    }
}
