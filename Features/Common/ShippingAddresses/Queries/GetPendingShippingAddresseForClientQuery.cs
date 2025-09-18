using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ShippingAddresses;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Queries
{
    public record GetPendingShippingAddresseForClientQuery(string ClientId) : IRequestBase<GetShippingAddressesForClientDTO>;
    public class GetPendingShippingAddresseForClientQueryHandler : RequestHandlerBase<ShippingAddress, GetPendingShippingAddresseForClientQuery, GetShippingAddressesForClientDTO>
    {
        public GetPendingShippingAddresseForClientQueryHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetShippingAddressesForClientDTO>> Handle(GetPendingShippingAddresseForClientQuery request, CancellationToken cancellationToken)
        {
            var pendingAddress = await _repository
                .Get(s => s.ClientId == request.ClientId && s.Status == ShippingAddressStatus.Pending)
                .Map<GetShippingAddressesForClientDTO>()
                .FirstOrDefaultAsync();

            if (pendingAddress == null)
            {
                return RequestResult<GetShippingAddressesForClientDTO>.Failure(ErrorCode.NotFound);
            }

            return RequestResult<GetShippingAddressesForClientDTO>.Success(pendingAddress);
        }
    }
}
