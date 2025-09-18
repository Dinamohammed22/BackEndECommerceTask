using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Categories;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Categories.Queries
{
    public record GetCategoryChildrenQuery(string? ParentCategoryId): IRequestBase<IEnumerable<GetCategoryChildrenDTO>>;

    public class GetCategoryChildrenQueryHandler: RequestHandlerBase<Category, GetCategoryChildrenQuery, IEnumerable<GetCategoryChildrenDTO>>
    {
        public GetCategoryChildrenQueryHandler(RequestHandlerBaseParameters<Category> requestParameters): base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetCategoryChildrenDTO>>> Handle(GetCategoryChildrenQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.Get(c =>
                c.IsActive &&
                c.ParentCategoryId == request.ParentCategoryId)
                .Include(c => c.Subcategories)
                .ToListAsync(cancellationToken);
            var result = categories.MapList<GetCategoryChildrenDTO>();
            //var result = categories.Select(c => new GetCategoryChildrenDTO(
            //    c.ID,
            //    c.Name,
            //    c.Subcategories != null && c.Subcategories.Any(sc => sc.IsActive)
            //));

            return RequestResult<IEnumerable<GetCategoryChildrenDTO>>.Success(result);
        }
    }
}
