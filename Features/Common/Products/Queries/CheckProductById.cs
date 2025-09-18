using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record CheckProductById(string ID):IRequestBase<string>;
    public class CheckProductByIdGetProductByIdHandler : RequestHandlerBase<Product, CheckProductById, string>
    {
        public CheckProductByIdGetProductByIdHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(CheckProductById request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIDAsync(request.ID);
            if (product == null)
            {
                return RequestResult<string>.Failure(ErrorCode.ProductNotFound);
            }

            return RequestResult<string>.Success(product.ID);

        }
    }
}
