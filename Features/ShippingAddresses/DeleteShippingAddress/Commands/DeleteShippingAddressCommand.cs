using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Governorates;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.ShippingAddresses.DeleteShippingAddress.Commands
{
    public record DeleteShippingAddressCommand(string ID):IRequestBase<bool>;
    public class DeleteShippingAddressCommandhandler : RequestHandlerBase<ShippingAddress, DeleteShippingAddressCommand, bool>
    {
        public DeleteShippingAddressCommandhandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeleteShippingAddressCommand request, CancellationToken cancellationToken)
        {
            var shippingAddress = await _repository.AnyAsync(s=>s.ID==request.ID);
            if (!shippingAddress)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            _repository.Delete(request.ID);
            _repository.SaveChanges();
            return await Task.FromResult(RequestResult<bool>.Success(true));
        }
    }
}
