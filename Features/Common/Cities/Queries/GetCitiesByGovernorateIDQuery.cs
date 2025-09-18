using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Common.Cities.Queries
{
    public record GetCitiesByGovernorateIDQuery(string GovernorateId) : IRequestBase<IEnumerable<GetCitiesByGovernorateIDProfileDTO>>;
    public class GetCitiesByGovernorateIDQueryHandler : RequestHandlerBase<City, GetCitiesByGovernorateIDQuery, IEnumerable<GetCitiesByGovernorateIDProfileDTO>>
    {
        public GetCitiesByGovernorateIDQueryHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<IEnumerable<GetCitiesByGovernorateIDProfileDTO>>> Handle(GetCitiesByGovernorateIDQuery request, CancellationToken cancellationToken)
        {
                var cities = _repository
                .Get(c => c.GovernorateId == request.GovernorateId).OrderBy(c => c.Name)
                .MapList<GetCitiesByGovernorateIDProfileDTO>();

                return RequestResult<IEnumerable<GetCitiesByGovernorateIDProfileDTO>>.Success(cities);
            
        }
    }
}
