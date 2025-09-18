using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.ShippingAddresses;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Queries
{
    public record GetDefualtShippingAddressForClientQuery(string ClientId):IRequestBase<GetDefualtShippingAddressForClientDTO>;
    public class GetDefualtShippingAddressForClientQueryHandler : RequestHandlerBase<ShippingAddress, GetDefualtShippingAddressForClientQuery, GetDefualtShippingAddressForClientDTO>
    {
        public GetDefualtShippingAddressForClientQueryHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetDefualtShippingAddressForClientDTO>> Handle(GetDefualtShippingAddressForClientQuery request, CancellationToken cancellationToken)
        {
            var shippingAddress = await _repository.Get(s => s.ClientId == request.ClientId && s.IsDefualt).Map<GetDefualtShippingAddressForClientDTO>().FirstOrDefaultAsync();
            return RequestResult<GetDefualtShippingAddressForClientDTO>.Success(shippingAddress);
        }
    }
}
