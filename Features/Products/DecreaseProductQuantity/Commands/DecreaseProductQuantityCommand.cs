using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.DecreaseProductQuantity.Commands
{
    public record DecreaseProductQuantityCommand(string ProductID , int QuantityToDecrease = 1) : IRequestBase<int>;
    public class DecreaseProductQuantityCommandHandler : RequestHandlerBase<Product, DecreaseProductQuantityCommand, int>
    {
        public DecreaseProductQuantityCommandHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<RequestResult<int>> Handle(DecreaseProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var product = _repository.GetByID(request.ProductID);

            if (product == null) 
            {
                return RequestResult<int>.Failure(ErrorCode.ProductNotFound);
            }

            if (product.Quantity < request.QuantityToDecrease)
            {
                return RequestResult<int>.Failure(ErrorCode.QuantityNotEnough);
            }

            product.Quantity -= request.QuantityToDecrease;

            _repository.SaveIncluded(product, nameof(product.Quantity));
            _repository.SaveChanges();

            return RequestResult<int>.Success(product.Quantity);
        }
    }
}
