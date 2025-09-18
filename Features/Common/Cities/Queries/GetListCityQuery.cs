using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Common.Cities.Queries;

public record GetListCityQuery(int pageIndex = 1, int pageSize = 100) : IRequestBase<PagingViewModel<CityProfileDTO>>;

public class GetListCityQueryHandler : RequestHandlerBase<City, GetListCityQuery, PagingViewModel<CityProfileDTO>>
{
    public GetListCityQueryHandler(RequestHandlerBaseParameters<City> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<PagingViewModel<CityProfileDTO>>> Handle(GetListCityQuery request, CancellationToken cancellationToken)
    {

        var city = 
            await _repository.Get().OrderBy(c => c.Name)
            .Map<CityProfileDTO>()
            .ToPagesAsync(request.pageIndex, request.pageSize);

        return RequestResult<PagingViewModel<CityProfileDTO>>.Success(city);
    }
}
