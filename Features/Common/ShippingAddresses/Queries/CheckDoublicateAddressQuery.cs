using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ShippingAddresses;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Queries
{
    public record CheckDoublicateAddressQuery(string? ShippingAddressId ,string ClientId,string GovernorateId, string CityId, string Street, string BuildingData):IRequestBase<bool>;
    public class CheckDoublicateAddressQueryHandler : RequestHandlerBase<ShippingAddress, CheckDoublicateAddressQuery, bool>
    {
        public CheckDoublicateAddressQueryHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckDoublicateAddressQuery request, CancellationToken cancellationToken)
        {
            var existingAddresses = await _mediator.Send(new GetShippingAddressesForClientQuery(request.ClientId));
            bool isDuplicate = existingAddresses.Data
                .Any(address => address.GovernorateId == request.GovernorateId &&
                                address.CityId == request.CityId &&
                                address.Street == request.Street &&
                                address.BuildingData == request.BuildingData &&
                                address.ID != request.ShippingAddressId); 

            if (isDuplicate)
            {
                return RequestResult<bool>.Failure(ErrorCode.DoublicateShippingAddress);
            }

            return RequestResult<bool>.Success(true);
        }
    }

}
