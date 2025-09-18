using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Queries
{
    public record CheckExistingAddressCountQuery(string ClientId):IRequestBase<bool>;
    public class CheckExistingAddressCountQueryHandler : RequestHandlerBase<ShippingAddress, CheckExistingAddressCountQuery, bool>
    {
        public CheckExistingAddressCountQueryHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckExistingAddressCountQuery request, CancellationToken cancellationToken)
        {
            var existingAddressCount = _repository.
                                               Get(sa => sa.ClientId == request.ClientId)
                                               .Count();

            if (existingAddressCount >= 3)
            {
                return RequestResult<bool>.Failure(ErrorCode.AlreadyHasThreeShippingAddresses);
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
