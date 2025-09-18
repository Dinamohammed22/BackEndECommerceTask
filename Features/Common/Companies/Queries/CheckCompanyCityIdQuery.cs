using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Common.Companies.Queries
{
    public record CheckCompanyCityIdQuery(string ID) : IRequestBase<bool>;
    public class CheckCompanyCityIdQueryHandler : RequestHandlerBase<Company, CheckCompanyCityIdQuery, bool>
    {
        public CheckCompanyCityIdQueryHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckCompanyCityIdQuery request, CancellationToken cancellationToken)
        {
            var ISExist = await _repository.AnyAsync(c => c.CityId == request.ID);
            return RequestResult<bool>.Success(ISExist);
        }
    }
}
