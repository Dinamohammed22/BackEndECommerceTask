using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Products.DeactivateProducts.Commands;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.BulkDeactivateProduct.Orchistrator
{
    public record BulkDeactivateProductOrchistrator(List<string> IDs) : IRequestBase<bool>;
    public class BulkDeactivateProductOrchistratorHandler : RequestHandlerBase<Product, BulkDeactivateProductOrchistrator, bool>
    {
        public BulkDeactivateProductOrchistratorHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkDeactivateProductOrchistrator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.IDs)
            {
                var result = await _mediator.Send(new DeactivateProductsCommand(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.CannotDelete);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
