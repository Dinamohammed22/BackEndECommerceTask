using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Products.ActivateProducts.Commands;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.BulkActivateProduct.Orchistrator
{
    public record BulkActivateProductOrchistrator(List<string> IDs):IRequestBase<bool>;
    public class BulkActivateProductOrchistratorHandler : RequestHandlerBase<Product, BulkActivateProductOrchistrator, bool>
    {
        public BulkActivateProductOrchistratorHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkActivateProductOrchistrator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.IDs)
            {
                var result = await _mediator.Send(new ActivateProductsCommand(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.CannotDelete);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
