using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.CartProducts.AddProductToCart.Commands;
using KOG.ECommerce.Features.Catrs.AddProductToCart.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Carts;

namespace KOG.ECommerce.Features.Catrs.AddCart.Orchestrators
{
    public record AddProductToCartOrchestrator(string ProductId, int Quantity):IRequestBase<bool>;
    public class AddProductToCartOrchestratorHandler : RequestHandlerBase<Cart, AddProductToCartOrchestrator, bool>
    {
        public AddProductToCartOrchestratorHandler(RequestHandlerBaseParameters<Cart> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(AddProductToCartOrchestrator request, CancellationToken cancellationToken)
        {
            var CartRequest = await _mediator.Send(request.MapOne<AddCartCommand>());
            var AddProduct = await _mediator.Send(request.MapOne<AddProductToCartCommand>());
            return RequestResult<bool>.Success(AddProduct.Data);
        }
    }
}
