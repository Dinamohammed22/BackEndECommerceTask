using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.ShippingAddresses.ApproveShippingAddress.Commands
{
    public record ApproveShippingAddressCommand(string ShippingAddressId) :IRequestBase<bool>;
    public class ApproveShippingAddressCommandHanler : RequestHandlerBase<ShippingAddress, ApproveShippingAddressCommand, bool>
    {
        public ApproveShippingAddressCommandHanler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ApproveShippingAddressCommand request, CancellationToken cancellationToken)
        {
            var shippingAddress= await _repository.AnyAsync(c=>c.ID==request.ShippingAddressId);
            if (shippingAddress != null) {
                ShippingAddress address = new ShippingAddress { ID = request.ShippingAddressId, Status = ShippingAddressStatus.Approved };
                _repository.SaveIncluded(address, nameof(address.Status));
                _repository.SaveChanges();
                return RequestResult<bool>.Success(true);
            }
            return RequestResult<bool>.Failure( ErrorCode.NotFound);


        }
    }
}
