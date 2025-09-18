using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Products.DeleteProduct.Commands;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.BulkDeleteProduct.Orchistrator
{
    public record BulkDeleteProductOrchistrator(List<string> Ids) : IRequestBase<bool>;
    public class BulkDeleteProductOrchistratorHandler : RequestHandlerBase<Product, BulkDeleteProductOrchistrator, bool>
    {
        public BulkDeleteProductOrchistratorHandler(RequestHandlerBaseParameters<Product> parameters) : base(parameters) { }

        public async override Task<RequestResult<bool>> Handle(BulkDeleteProductOrchistrator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var result = await _mediator.Send(new DeleteProductCommand(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.CannotDelete);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }

}
