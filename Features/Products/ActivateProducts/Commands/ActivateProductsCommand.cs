using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Products;
using MediatR.Wrappers;

namespace KOG.ECommerce.Features.Products.ActivateProducts.Commands
{
    public record ActivateProductsCommand(string Id) :IRequestBase<bool>;
    public class ActivateProductsCommandHandler : RequestHandlerBase<Product, ActivateProductsCommand, bool>
    {
        public ActivateProductsCommandHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActivateProductsCommand request, CancellationToken cancellationToken)
        {
           var check =await _repository.AnyAsync(p=>p.ID==request.Id);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Product product=new Product();
            product.ID=request.Id;
            product.IsActive = true;
            _repository.SaveIncluded(product, nameof(product.IsActive));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
