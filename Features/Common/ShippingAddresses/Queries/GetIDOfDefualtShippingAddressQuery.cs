using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.ShippingAddresses;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Queries
{
    public record GetIDOfDefualtShippingAddressQuery(string ClientId):IRequestBase<string>;
    public class GetIDOfDefualtShippingAddressQueryHandler : RequestHandlerBase<ShippingAddress, GetIDOfDefualtShippingAddressQuery, string>
    {
        public GetIDOfDefualtShippingAddressQueryHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(GetIDOfDefualtShippingAddressQuery request, CancellationToken cancellationToken)
        {
           var ID= await _repository.Get(s=>s.ClientId==request.ClientId && s.IsDefualt).Select(s=>s.ID).FirstOrDefaultAsync();
            return RequestResult<string>.Success(ID);
        }
    }
}
