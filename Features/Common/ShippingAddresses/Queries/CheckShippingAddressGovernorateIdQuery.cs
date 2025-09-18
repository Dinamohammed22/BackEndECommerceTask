using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Queries
{
    public record CheckShippingAddressGovernorateIdQuery(string ID) : IRequestBase<bool>;
    public class CheckShippingAddressGovernorateIdQueryHandler : RequestHandlerBase<ShippingAddress, CheckShippingAddressGovernorateIdQuery, bool>
    {
        public CheckShippingAddressGovernorateIdQueryHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckShippingAddressGovernorateIdQuery request, CancellationToken cancellationToken)
        {
            var ISExist = await _repository.AnyAsync(s => s.GovernorateId == request.ID);
            return RequestResult<bool>.Success(ISExist);
        }
    }
}
