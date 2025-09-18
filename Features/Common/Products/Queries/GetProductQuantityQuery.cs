using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetProductQuantityQuery(string ID):IRequestBase<int>;
    public class GetProductQuantityQueryHandller : RequestHandlerBase<Product, GetProductQuantityQuery, int>
    {
        public GetProductQuantityQueryHandller(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<RequestResult<int>> Handle(GetProductQuantityQuery request, CancellationToken cancellationToken)
        {
            int ProductQuantity = _repository.GetByID(request.ID).Quantity;
            return RequestResult<int>.Success(ProductQuantity);
        }
    }
}
