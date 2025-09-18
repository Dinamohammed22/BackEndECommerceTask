using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.RestockProduct.commands
{
    public record RestockProductCommand(string ProductID , int Quantity=1):IRequestBase<bool>;
    public class RestockProductCommandHandler : RequestHandlerBase<Product, RestockProductCommand, bool>
    {
        public RestockProductCommandHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<RequestResult<bool>> Handle(RestockProductCommand request, CancellationToken cancellationToken)
        {
            var product = _repository.GetByID(request.ProductID);
            if (product == null)
            {
                return RequestResult<bool>.Failure(ErrorCode.ProductNotFound);
            }
            product.Quantity = request.Quantity;
            _repository.SaveIncluded(product, nameof(product.Quantity));
            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);
        }
    }
}
