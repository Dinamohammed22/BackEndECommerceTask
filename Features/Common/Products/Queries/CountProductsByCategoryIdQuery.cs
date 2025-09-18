using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record CountProductsByCategoryIdQuery(string CategoryId):IRequestBase<int>;
    public class CountProductsByCategoryIdQueryHandler : RequestHandlerBase<Product, CountProductsByCategoryIdQuery, int>
    {
        public CountProductsByCategoryIdQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<int>> Handle(CountProductsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var count = _repository.Get(c => c.CategoryId == request.CategoryId).Count();

            if (count == 0)
            {
                return RequestResult<int>.Success(0); // Explicitly return 0 if no products exist
            }

            return RequestResult<int>.Success(count); // Ensure a valid integer count is returned
        }
    }

}
