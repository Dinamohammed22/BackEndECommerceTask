using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Queries
{
    public record CheckShippingAddressCityIdQuery(string ID) : IRequestBase<bool>;
    public class CheckShippingAddressCityIdQueryHandler : RequestHandlerBase<ShippingAddress, CheckShippingAddressCityIdQuery, bool>
    {
        public CheckShippingAddressCityIdQueryHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckShippingAddressCityIdQuery request, CancellationToken cancellationToken)
        {
            var ISExist = await _repository.AnyAsync(s => s.CityId == request.ID);
            return RequestResult<bool>.Success(ISExist);
        }
    }
}
