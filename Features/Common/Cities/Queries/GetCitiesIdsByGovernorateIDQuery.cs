using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Cities;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Cities.Queries
{
    public record GetCitiesIdsByGovernorateIDQuery(string ID):IRequestBase<IEnumerable<string>>;
    public class GetCitiesIdsByGovernorateIDQueryHandler : RequestHandlerBase<City, GetCitiesIdsByGovernorateIDQuery, IEnumerable<string>>
    {
        public GetCitiesIdsByGovernorateIDQueryHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<string>>> Handle(GetCitiesIdsByGovernorateIDQuery request, CancellationToken cancellationToken)
        {
            var citiesIds = await _repository.Get(c => c.GovernorateId == request.ID)
                .Select(c => c.ID )
                .ToListAsync();
            return await Task.FromResult(RequestResult<IEnumerable<string>>.Success(citiesIds));
        }
    }

}
