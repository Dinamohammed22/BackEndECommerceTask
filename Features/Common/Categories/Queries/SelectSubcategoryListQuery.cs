using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Models.Categories;

namespace KOG.ECommerce.Features.Common.Categories.Queries
{
    public record SelectSubcategoryListQuery(string CategoryId) : IRequestBase<IEnumerable<SelectListItemViewModel>>;
    public class SelectSubcategoryListQueryHandler : RequestHandlerBase<Category, SelectSubcategoryListQuery, IEnumerable<SelectListItemViewModel>>
    {
        public SelectSubcategoryListQueryHandler(RequestHandlerBaseParameters<Category> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<IEnumerable<SelectListItemViewModel>>> Handle(SelectSubcategoryListQuery request, CancellationToken cancellationToken)
        {
           
            var Subcategories =  _repository.Get(c => c.ParentCategoryId == request.CategoryId).ToSelectListViewModel();

            return RequestResult<IEnumerable<SelectListItemViewModel>>.Success(Subcategories);
        }
    }

}
