using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.Queries;

public record CheckIfCategoryHasProductsQuery(string ID) : IRequestBase<bool>;
public class CheckIfCategoryHasProductsQueryHandler : RequestHandlerBase<Product, CheckIfCategoryHasProductsQuery, bool>
{
    public CheckIfCategoryHasProductsQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
    {
    }

    public async override Task<RequestResult<bool>> Handle(CheckIfCategoryHasProductsQuery request, CancellationToken cancellationToken)
    {
        var findCategoryId = await _repository.AnyAsync(c => c.CategoryId == request.ID);
        return RequestResult<bool>.Success(findCategoryId);
    }
}

