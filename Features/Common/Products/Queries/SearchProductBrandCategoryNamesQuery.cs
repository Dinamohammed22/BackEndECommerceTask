using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record SearchProductBrandCategoryNamesQuery(string Name) : IRequestBase<IEnumerable<string>>;

    public class SearchProductBrandCategoryNamesQueryHandler
        : RequestHandlerBase<Product, SearchProductBrandCategoryNamesQuery, IEnumerable<string>>
    {
        public SearchProductBrandCategoryNamesQueryHandler(RequestHandlerBaseParameters<Product> requestParameters)
            : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<string>>> Handle(
            SearchProductBrandCategoryNamesQuery request,
            CancellationToken cancellationToken)
        {
            var searchTerm = request.Name?.ToLower();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RequestResult<IEnumerable<string>>.Failure(ECommerce.Common.Enums.ErrorCode.NotFound);
            }

            // Fetch products along with their categories and brands
            var products = await _repository
                .Get()
                .Include(c => c.Category)
                .Include(b => b.Brand)
                .ToListAsync(cancellationToken);

            // Now filter and select the names in memory (client-side evaluation)
            var matchingNames = products
                .SelectMany(p => new[]
                {
                    // Return the Name if it matches the search term
                    p.Name != null && p.Name.ToLower().Contains(searchTerm) ? p.Name : null,
                    // Return the Category Name if it matches the search term
                    p.Category != null && p.Category.Name != null && p.Category.Name.ToLower().Contains(searchTerm) ? p.Category.Name : null,
                    // Return the Brand Name if it matches the search term
                    p.Brand != null && p.Brand.Name != null && p.Brand.Name.ToLower().Contains(searchTerm) ? p.Brand.Name : null
                })
                .Where(name => !string.IsNullOrEmpty(name)) // Filter out null or empty names
                .Distinct() // Ensure unique names
                .ToList();

            // If no matching names were found
            if (matchingNames == null || matchingNames.Count == 0)
            {
                return RequestResult<IEnumerable<string>>.Failure(ECommerce.Common.Enums.ErrorCode.NotFound);
            }

            // Return the matching names
            return RequestResult<IEnumerable<string>>.Success(matchingNames);
        }
    }
}
