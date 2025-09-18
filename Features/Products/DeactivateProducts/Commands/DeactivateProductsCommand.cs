using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.DeactivateProducts.Commands
{
    public record DeactivateProductsCommand(string Id):IRequestBase<bool>;
    public class DeactivateProductsCommandHandler : RequestHandlerBase<Product, DeactivateProductsCommand, bool>
    {
        public DeactivateProductsCommandHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeactivateProductsCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(p => p.ID == request.Id);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Product product=new Product();
            product.ID = request.Id;
            product.IsActive = false;
            _repository.SaveIncluded(product, nameof(product.IsActive));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
