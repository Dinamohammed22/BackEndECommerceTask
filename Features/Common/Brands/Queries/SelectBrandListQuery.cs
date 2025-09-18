using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Models.Brands;

namespace KOG.ECommerce.Features.Common.Brands.Queries
{
    public record SelectBrandListQuery():IRequestBase<IEnumerable<SelectListItemViewModel>>;
    public class SelectBrandListQueryHandler : RequestHandlerBase<Brand, SelectBrandListQuery, IEnumerable<SelectListItemViewModel>>
    {
        public SelectBrandListQueryHandler(RequestHandlerBaseParameters<Brand> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<SelectListItemViewModel>>> Handle(SelectBrandListQuery request, CancellationToken cancellationToken)
        {
            var selectListItems = _repository.Get().ToSelectListViewModel();
             return RequestResult<IEnumerable<SelectListItemViewModel>>.Success(selectListItems);
        }
    }
}
