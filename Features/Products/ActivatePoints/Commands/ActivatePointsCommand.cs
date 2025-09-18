using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.ActivatePoints.Commands
{
    public record ActivatePointsCommand(string ID):IRequestBase<bool>;
    public class ActivatePointsCommandHandler : RequestHandlerBase<Product, ActivatePointsCommand, bool>
    {
        public ActivatePointsCommandHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActivatePointsCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(p => p.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.ProductNotFound);
            Product product = new Product();
            product.ID = request.ID;
            product.IsActivePoint = true;
            _repository.SaveIncluded(product, nameof(product.IsActivePoint));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
