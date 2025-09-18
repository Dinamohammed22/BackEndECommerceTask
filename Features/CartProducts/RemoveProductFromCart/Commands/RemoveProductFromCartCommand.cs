using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.CartProducts;
using KOG.ECommerce.Models.Governorates;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.CartProducts.RemoveProductFromCart.Commands
{
    public record RemoveProductFromCartCommand(string ProductId):IRequestBase<bool>;
    public class RemoveProductFromCartCommandHandler : RequestHandlerBase<CartProduct, RemoveProductFromCartCommand, bool>
    {
        public RemoveProductFromCartCommandHandler(RequestHandlerBaseParameters<CartProduct> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(RemoveProductFromCartCommand request, CancellationToken cancellationToken)
        {
            CartProduct cartProduct = await _repository.Get(cp=>cp.ProductId==request.ProductId && cp.CartId==_userState.UserID).FirstOrDefaultAsync();
            if (cartProduct == null)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            _repository.Delete(cartProduct);
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }

}
