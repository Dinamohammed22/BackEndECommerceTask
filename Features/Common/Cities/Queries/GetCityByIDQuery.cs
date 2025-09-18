using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.Governorates;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Cities.Queries
{
    public record GetCityByIDQuery(string ID) : IRequestBase<GetCityByIDProfileDTO>;
    public class GetCityByIDQueryHandler : RequestHandlerBase<City, GetCityByIDQuery, GetCityByIDProfileDTO>
    {
        public GetCityByIDQueryHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<GetCityByIDProfileDTO>> Handle(GetCityByIDQuery request, CancellationToken cancellationToken)
        {
            var city =await _repository
                .Get(c => c.ID == request.ID)
                .Map<GetCityByIDProfileDTO>()
                .FirstOrDefaultAsync();

            return RequestResult<GetCityByIDProfileDTO>.Success(city);
        }
        }

}
