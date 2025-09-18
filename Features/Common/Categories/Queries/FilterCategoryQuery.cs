using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Categories;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Categories.Queries
{
    public record FilterCategoryQuery(string? Name) : IRequestBase<IEnumerable<CategoryFilterDTO>>;
    public class FilterCategoryQueryHandler : RequestHandlerBase<Category, FilterCategoryQuery, IEnumerable<CategoryFilterDTO>>
    {
        public FilterCategoryQueryHandler(RequestHandlerBaseParameters<Category> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<CategoryFilterDTO>>> Handle(FilterCategoryQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<Category>(true);
            predicate = predicate.And(c => string.IsNullOrEmpty(request.Name) ? true : c.Name.Contains(request.Name));

            IEnumerable<CategoryFilterDTO> query = _repository.Get(predicate).Where(c=>c.IsActive).MapList<CategoryFilterDTO>();

            return RequestResult<IEnumerable<CategoryFilterDTO>>.Success(query);
        }
    }
}
