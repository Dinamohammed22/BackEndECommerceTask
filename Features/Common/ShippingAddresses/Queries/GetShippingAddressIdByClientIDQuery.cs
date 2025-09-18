using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.ShippingAddresses;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Queries
{
    public record GetShippingAddressIdByClientIDQuery(string ClientId):IRequestBase<string>;
    public class GetShippingAddressIdByClientIDQueryHandler : RequestHandlerBase<ShippingAddress, GetShippingAddressIdByClientIDQuery, string>
    {
        public GetShippingAddressIdByClientIDQueryHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<string>> Handle(GetShippingAddressIdByClientIDQuery request, CancellationToken cancellationToken)
        {
            string shippingAddressId = await _repository.Get(s => s.ClientId == request.ClientId)
                .Select(s => s.ID)
                .FirstOrDefaultAsync(); ;

            if (string.IsNullOrEmpty(shippingAddressId))
            {
                return RequestResult<string>.Failure(ErrorCode.ShippingAddressNotFound);
            }

            return RequestResult<string>.Success(shippingAddressId);

        }
    }

}
