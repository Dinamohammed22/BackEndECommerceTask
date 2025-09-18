using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record CheckIfProductHasBrandQuery(string ID) : IRequestBase<bool>;
    public class CheckIfProductHasBrandQueryHandler : RequestHandlerBase<Product, CheckIfProductHasBrandQuery, bool>
    {
        public CheckIfProductHasBrandQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckIfProductHasBrandQuery request, CancellationToken cancellationToken)
        {
            var findClassificationId = await _repository.AnyAsync(c => c.BrandId == request.ID);
            return RequestResult<bool>.Success(findClassificationId);
        }
    }

}
