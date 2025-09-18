using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.ShippingAddresses.RejectShippingAddress.Commands
{
    public record RejectShippingAddressCommand(string ShippingAddressId) : IRequestBase<bool>;
    public class RejectShippingAddressCommandHandler : RequestHandlerBase<ShippingAddress, RejectShippingAddressCommand, bool>
    {
        public RejectShippingAddressCommandHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(RejectShippingAddressCommand request, CancellationToken cancellationToken)
        {
            var shippingAddress =await _repository.AnyAsync(c => c.ID == request.ShippingAddressId);
            if (shippingAddress != null)
            {
                ShippingAddress address = new ShippingAddress { ID = request.ShippingAddressId };
                address.Status = ShippingAddressStatus.Rejected;
                _repository.SaveIncluded(address, nameof(address.Status));
                _repository.SaveChanges();
                return RequestResult<bool>.Success(true);
            }
            return RequestResult<bool>.Failure(ErrorCode.NotFound);


        }
    }
}
