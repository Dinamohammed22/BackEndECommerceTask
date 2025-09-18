using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Categories;
using PredicateExtensions;

namespace KOG.ECommerce.Features.Common.Categories.Queries
{
    public record GetCategoryQuery(
    ) : IRequestBase<IEnumerable<CategoryDTO>>;

    public class GetFilteredCategoriesQueryHandler : RequestHandlerBase<Category, GetCategoryQuery, IEnumerable<CategoryDTO>>
    {
        public GetFilteredCategoriesQueryHandler(RequestHandlerBaseParameters<Category> parameters) : base(parameters)
        {
        }
        public async override Task<RequestResult<IEnumerable<CategoryDTO>>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = (_repository.Get().Where(c=>c.IsActive)).Select(category => new CategoryDTO(
                category.ID,
                category.Name,
                category.ParentCategoryId,
                new List<string>(),
                new List<CategoryDTO>()
            )).ToList();

            var categoryDictionary = categories.ToDictionary(c => c.Id);

            var rootCategories = new List<CategoryDTO>();

            foreach (var category in categories)
            {
                if (category.ParentCategoryId == null)
                {

                    rootCategories.Add(category);
                }
                else if (categoryDictionary.ContainsKey(category.ParentCategoryId))
                {
                    var parentCategory = categoryDictionary[category.ParentCategoryId];
                    parentCategory.Subcategories.Add(category);
                }
            }

            return RequestResult<IEnumerable<CategoryDTO>>.Success(rootCategories);
        }

    }
}