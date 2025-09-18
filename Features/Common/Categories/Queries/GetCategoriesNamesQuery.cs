using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Models.Categories;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Categories.Queries
{
    public record GetCategoriesNamesQuery:IRequestBase<IEnumerable<GetCategoriesNamesDTO>>;
    public class GetCategoriesNamesQueryHandler : RequestHandlerBase<Category, GetCategoriesNamesQuery, IEnumerable<GetCategoriesNamesDTO>>
    {
        public GetCategoriesNamesQueryHandler(RequestHandlerBaseParameters<Category> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetCategoriesNamesDTO>>> Handle(GetCategoriesNamesQuery request, CancellationToken cancellationToken)
        {
            var parentCategories = await _repository.Get(c => c.ParentCategoryId == null)
            .Include(c => c.Subcategories)
            .ThenInclude(sc => sc.products)
            .Include(c => c.products)
            .ToListAsync();

            var result = parentCategories
              .Where(c => GetTotalProducts(c) > 0)
              .Select(c => new GetCategoriesNamesDTO(
                  c.ID,
                  c.Name,
                  GetTotalProducts(c)
              ))
              .ToList();
            return RequestResult<IEnumerable<GetCategoriesNamesDTO>>.Success(result);
        }
        private static int GetTotalProducts(Category category)
        {
            var totalProducts = category.products?.Count ?? 0;
            if (category.Subcategories != null)
            {
                totalProducts += category.Subcategories.Sum(GetTotalProducts);
            }
            return totalProducts;
        }
    }


}
