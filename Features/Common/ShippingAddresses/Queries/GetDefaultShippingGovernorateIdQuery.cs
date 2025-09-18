using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.ShippingAddresses;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Queries;

public sealed record GetDefaultShippingGovernorateIdQuery(string ClientId): IRequestBase<string>;

public sealed class GetDefaultShippingGovernorateIdQueryHandler : RequestHandlerBase<ShippingAddress, GetDefaultShippingGovernorateIdQuery, string>
{
    public GetDefaultShippingGovernorateIdQueryHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
    {
    }

    public async override Task<RequestResult<string>> Handle(GetDefaultShippingGovernorateIdQuery request, CancellationToken cancellationToken)
    {
        var governorateId = await _repository
            .Get(s => s.ClientId == request.ClientId && s.IsDefualt)
            .Select(s => s.GovernorateId)
            .FirstOrDefaultAsync();

        return governorateId is null
            ? RequestResult<string>.Success(null)
            : RequestResult<string>.Success(governorateId);
    }
}
