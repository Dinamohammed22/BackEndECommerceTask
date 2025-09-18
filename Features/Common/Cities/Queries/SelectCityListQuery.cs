using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Common.Cities.Queries
{
    public record SelectCityListQuery(string? GovernorateId):IRequestBase<IEnumerable<SelectListItemViewModel>>;
    public class SelectCityListQueryHandler : RequestHandlerBase<City, SelectCityListQuery, IEnumerable<SelectListItemViewModel>>
    {
        public SelectCityListQueryHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<SelectListItemViewModel>>> Handle(SelectCityListQuery request, CancellationToken cancellationToken)
        {
            var selectListItems = _repository.Get(c=>c.GovernorateId==request.GovernorateId).ToSelectListViewModel();
            if (request.GovernorateId == null)
                selectListItems = _repository.Get().ToSelectListViewModel();
            return RequestResult<IEnumerable<SelectListItemViewModel>>.Success(selectListItems);
        }
    }
}
