using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Carts;

namespace KOG.ECommerce.Features.Carts.DeleteCart.Commands
{
     public record DeleteCartCommand(string ID) : IRequestBase<bool>;
    public class DeleteCartCommandHandler : RequestHandlerBase<Cart, DeleteCartCommand, bool>
    {
        public DeleteCartCommandHandler(RequestHandlerBaseParameters<Cart> parameters) : base(parameters) { }

        public async override Task<RequestResult<bool>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(p => p.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Cart Cart = new Cart();
            Cart.ID = request.ID;
            _repository.Delete(request.ID);
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
