using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Categories;
using MediatR.Wrappers;

namespace KOG.ECommerce.Features.Common.Categories.Queries
{
    public record SelectCategoryListQuery():IRequestBase<IEnumerable<SelectListItemViewModel>>;
    public class SelectCategoryListQueryHandler : RequestHandlerBase<Category, SelectCategoryListQuery, IEnumerable<SelectListItemViewModel>>
    {
        public SelectCategoryListQueryHandler(RequestHandlerBaseParameters<Category> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<SelectListItemViewModel>>> Handle(SelectCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categories=_repository.Get(c=>c.ParentCategoryId==null).ToSelectListViewModel();
            return RequestResult<IEnumerable<SelectListItemViewModel>>.Success(categories);
        }
    }
}
