using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.CartProducts;
using KOG.ECommerce.Models.Carts;

namespace KOG.ECommerce.Features.Catrs.AddProductToCart.Commands
{
    public record AddCartCommand() : IRequestBase<bool>;
    public class AddCartCommandHandler : RequestHandlerBase<Cart, AddCartCommand, bool>
    {
        public AddCartCommandHandler(RequestHandlerBaseParameters<Cart> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddCartCommand request, CancellationToken cancellationToken)
        {
            var ExistCart = _repository.Any(c=>c.ID==_userState.UserID);
            if (!ExistCart)
            {
                Cart cart = new Cart { ID=_userState.UserID};
                _repository.Add(cart);
                _repository.SaveChanges();
                return RequestResult<bool>.Success(true);
            }
            return RequestResult<bool>.Success(false);
        }
    }
}
