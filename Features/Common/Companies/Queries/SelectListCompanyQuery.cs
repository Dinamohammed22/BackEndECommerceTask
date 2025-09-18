using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Common.Companies.Queries
{
    public record SelectListCompanyQuery(): IRequestBase<IEnumerable<SelectListItemViewModel>>;
    public class SelectListCompanyQueryHandler : RequestHandlerBase<Company, SelectListCompanyQuery, IEnumerable<SelectListItemViewModel>>
    {
        public SelectListCompanyQueryHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<SelectListItemViewModel>>> Handle(SelectListCompanyQuery request, CancellationToken cancellationToken)
        {
            var selectListItems = _repository.Get().ToSelectListViewModel();
            return RequestResult<IEnumerable<SelectListItemViewModel>>.Success(selectListItems);
        }
    }
}
