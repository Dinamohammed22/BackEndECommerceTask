using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.CartProducts;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.CartProducts.RemoveProductFromCart
{
    public record RemoveAllProductsFromCartCommand(string? CartId) : IRequestBase<bool>;

    public class RemoveAllProductsFromCartCommandHandler : RequestHandlerBase<CartProduct, RemoveAllProductsFromCartCommand, bool>
    {
        public RemoveAllProductsFromCartCommandHandler(RequestHandlerBaseParameters<CartProduct> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(RemoveAllProductsFromCartCommand request, CancellationToken cancellationToken)
        {
            var ID = request.CartId is null ? _userState.UserID : request.CartId;

            var cartProducts =  _repository.Get(cp => cp.CartId == ID ).ToList();

            if (cartProducts == null || !cartProducts.Any())
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            foreach (var cartProduct in cartProducts)
            {
                _repository.Delete(cartProduct);
            }

            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);
        }
    }
}
