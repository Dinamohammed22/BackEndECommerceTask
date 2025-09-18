using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.ShippingAddresses;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Queries
{
    public record GetShippingAddresseIdQuery(string ID):IRequestBase<GetShippingAddresseIdDTO>;
    public class GetShippingAddresseIdQueryHandler : RequestHandlerBase<ShippingAddress, GetShippingAddresseIdQuery, GetShippingAddresseIdDTO>
    {
        public GetShippingAddresseIdQueryHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<GetShippingAddresseIdDTO>> Handle(GetShippingAddresseIdQuery request, CancellationToken cancellationToken)
        {
            // Include Governorate and City in the query to fetch their names
            var shippingAddress =  _repository
                .Get(s=>s.ID==request.ID).Include(sa => sa.Governorate).Include(sa => sa.City).FirstOrDefault();

         var result = new GetShippingAddresseIdDTO(
         GovernorateName: shippingAddress.Governorate?.Name,
         GovernorateId: shippingAddress.GovernorateId,
         CityName: shippingAddress.City?.Name,
         CityId: shippingAddress.CityId,
         Street: shippingAddress.Street,
         Landmark: shippingAddress.Landmark,
         Latitude: shippingAddress.Latitude,
         Longitude: shippingAddress.Longitude,
         IsDefualt: shippingAddress.IsDefualt,
         BuildingData:shippingAddress.BuildingData,
         Status:shippingAddress.Status
     );

            return RequestResult<GetShippingAddresseIdDTO>.Success(result);
        }

    }
}
