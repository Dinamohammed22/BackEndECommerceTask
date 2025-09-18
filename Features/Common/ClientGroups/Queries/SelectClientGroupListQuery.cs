using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.ClientGroups;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.Common.ClientGroups.Queries
{
    public record SelectClientGroupListQuery : IRequestBase<IEnumerable<SelectListItemViewModel>>;
    public class SelectClientGroupListQueryHandler : RequestHandlerBase<ClientGroup, SelectClientGroupListQuery, IEnumerable<SelectListItemViewModel>>
    {
        public SelectClientGroupListQueryHandler(RequestHandlerBaseParameters<ClientGroup> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<SelectListItemViewModel>>> Handle(SelectClientGroupListQuery request, CancellationToken cancellationToken)
        {
            var selectListItems = _repository.Get().ToSelectListViewModel();
            return RequestResult<IEnumerable<SelectListItemViewModel>>.Success(selectListItems);
        }
    }

}
