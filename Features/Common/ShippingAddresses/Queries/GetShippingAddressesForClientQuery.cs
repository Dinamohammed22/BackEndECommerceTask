using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.ShippingAddresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Queries
{
    public record GetShippingAddressesForClientQuery(string ClientId):IRequestBase<IEnumerable<GetShippingAddressesForClientDTO>>;
    public class GetShippingAddressesForClientQueryHandler : RequestHandlerBase<ShippingAddress, GetShippingAddressesForClientQuery, IEnumerable<GetShippingAddressesForClientDTO>>
    {
        public GetShippingAddressesForClientQueryHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetShippingAddressesForClientDTO>>> Handle(GetShippingAddressesForClientQuery request, CancellationToken cancellationToken)
        {
            var shippingAddresses = await _repository.Get(s => s.ClientId == request.ClientId).Map<GetShippingAddressesForClientDTO>().ToListAsync();
            var resultShippingAddresses = new List<GetShippingAddressesForClientDTO>();
            
            foreach (var shippingAddress in shippingAddresses)
            {
                var cityName = !string.IsNullOrEmpty(shippingAddress.CityId)
                    ? (await _mediator.Send(new GetCityByIDQuery(shippingAddress.CityId)))?.Data.Name ?? string.Empty
                    : "No City";

                var governorateName = !string.IsNullOrEmpty(shippingAddress.GovernorateId)
                    ? (await _mediator.Send(new GetGovernorateByIDQuery(shippingAddress.GovernorateId)))?.Data.Name ?? string.Empty
                    : "No Governorate";
                resultShippingAddresses.Add(new GetShippingAddressesForClientDTO(
                    ID:shippingAddress.ID,
                   GovernorateName: governorateName,
                   GovernorateId: shippingAddress.GovernorateId,
                   CityName: cityName,
                   CityId: shippingAddress.CityId,
                   Street: shippingAddress.Street,
                   Landmark: shippingAddress.Landmark,
                   Latitude: shippingAddress.Latitude,
                   Longitude: shippingAddress.Longitude,
                   Status:shippingAddress.Status,
                   BuildingData:shippingAddress.BuildingData,
                   IsDefualt:shippingAddress.IsDefualt
                   
               ));
            }
            return RequestResult<IEnumerable<GetShippingAddressesForClientDTO>>.Success(resultShippingAddresses);
        }
    }
}
