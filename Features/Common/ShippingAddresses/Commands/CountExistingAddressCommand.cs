using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Commands
{
    public record CountExistingAddressCommand(string ClientId) : IRequestBase<int>;
    public class CountExistingAddressCommandHandler : RequestHandlerBase<ShippingAddress, CountExistingAddressCommand, int>
    {
        public CountExistingAddressCommandHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<int>> Handle(CountExistingAddressCommand request, CancellationToken cancellationToken)
        {
            var existingAddressCount = _repository.
                                            Get(sa => sa.ClientId == request.ClientId)
                                            .Count();

            return RequestResult<int>.Success(existingAddressCount);
        }
    }
}
