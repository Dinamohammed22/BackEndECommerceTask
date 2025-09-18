using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.Companies;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Cities.Queries
{
    public record GetByCityNameOrGovernNameQuery(string? CityName=null, string? GovernorateId=null, int pageIndex = 1, int pageSize = 100) : IRequestBase<PagingViewModel<CityProfileDTO>>;


    public class GetByCityNameOrGovernNameQueryHandler : RequestHandlerBase<City, GetByCityNameOrGovernNameQuery, PagingViewModel<CityProfileDTO>>
    {
        public GetByCityNameOrGovernNameQueryHandler(RequestHandlerBaseParameters<City> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<PagingViewModel<CityProfileDTO>>> Handle(GetByCityNameOrGovernNameQuery request, CancellationToken cancellationToken)
        {
            
                var predicate = PredicateExtensions.PredicateExtensions.Begin<City>(true);

                predicate = predicate
                 .And(c => string.IsNullOrEmpty(request.CityName) || c.Name.Contains(request.CityName))
                 .And(c => string.IsNullOrEmpty(request.GovernorateId) || c.Governorate.ID==request.GovernorateId);

                var query =await _repository.Get(predicate).OrderBy(c=>c.Name)
                    .Map<CityProfileDTO>()
                    .ToPagesAsync(request.pageIndex, request.pageSize); ;

                return RequestResult<PagingViewModel<CityProfileDTO>>.Success(query);

  
        }
    }
}
