using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ShippingAddresses;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Queries
{
    public record GetIDOfPendingShippingAddressQuery(string ClientId) : IRequestBase<string>;
    public class GetIDOfPendingShippingAddressQueryHandler : RequestHandlerBase<ShippingAddress, GetIDOfPendingShippingAddressQuery, string>
    {
        public GetIDOfPendingShippingAddressQueryHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(GetIDOfPendingShippingAddressQuery request, CancellationToken cancellationToken)
        {
            var ID = await _repository.Get(s => s.ClientId == request.ClientId && s.Status== ShippingAddressStatus.Pending).Select(s => s.ID).FirstOrDefaultAsync();
            return RequestResult<string>.Success(ID);
        }
    }
}
