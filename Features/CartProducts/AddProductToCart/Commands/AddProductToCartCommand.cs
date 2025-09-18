using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.CartProducts;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;

namespace KOG.ECommerce.Features.CartProducts.AddProductToCart.Commands
{
    public record AddProductToCartCommand(string ProductId,int Quantity):IRequestBase<bool>;
    public class AddProductToCartCommandHandler : RequestHandlerBase<CartProduct, AddProductToCartCommand, bool>
    {
        public AddProductToCartCommandHandler(RequestHandlerBaseParameters<CartProduct> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
        {
            
            // Check if the product exists
            var checkProductResult = await _mediator.Send(new CheckProductById(request.ProductId));

            if (checkProductResult.Data.IsNullOrEmpty())
            {
                return RequestResult<bool>.Failure(ErrorCode.ProductNotFound);
            }

            var existProduct=_repository.Get(c=>c.ProductId == request.ProductId && c.CartId==_userState.UserID).FirstOrDefault();
            if (existProduct==null)
            {
                var NewProduct = new CartProduct { ProductId = request.ProductId, CartId = _userState.UserID, Quantity = request.Quantity };
                _repository.Add(NewProduct);
                _repository.SaveChanges();
            }
            else
            {
                existProduct.Quantity = request.Quantity;
                _repository.SaveIncluded(existProduct,nameof(existProduct.Quantity));
                _repository.SaveChanges();
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
