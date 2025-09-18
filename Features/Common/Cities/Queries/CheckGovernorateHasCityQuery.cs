using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Common.Cities.Queries;

public record CheckGovernorateHasCityQuery(string ID) : IRequestBase<bool>;

public class CheckGovernorateHasCityQueryHandler : RequestHandlerBase<City, CheckGovernorateHasCityQuery, bool>
{
    public CheckGovernorateHasCityQueryHandler(RequestHandlerBaseParameters<City> parameters) : base(parameters) { }

    public async override Task<RequestResult<bool>> Handle(CheckGovernorateHasCityQuery request, CancellationToken cancellationToken)
    {
        var findGovernorateId = await _repository.AnyAsync(c => c.GovernorateId == request.ID);
        return RequestResult<bool>.Success(findGovernorateId);
    }
}