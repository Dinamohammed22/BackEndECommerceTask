using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Models.Classifications;

namespace KOG.ECommerce.Features.Common.Classifications.Queries
{
    public record SelectListClassificationQuery():IRequestBase<IEnumerable<SelectListItemViewModel>>;
    public class SelectListClassificationQueryHandler : RequestHandlerBase<Classification, SelectListClassificationQuery, IEnumerable<SelectListItemViewModel>>
    {
        public SelectListClassificationQueryHandler(RequestHandlerBaseParameters<Classification> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<SelectListItemViewModel>>> Handle(SelectListClassificationQuery request, CancellationToken cancellationToken)
        {
            var selectListItems = _repository.Get().ToSelectListViewModel();
            return RequestResult<IEnumerable<SelectListItemViewModel>>.Success(selectListItems);

        }
    }
}
