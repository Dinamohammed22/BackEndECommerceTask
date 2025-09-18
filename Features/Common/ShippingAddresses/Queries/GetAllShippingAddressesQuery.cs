using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ShippingAddresses;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Queries
{
    public record GetAllShippingAddressesQuery(string? GovernorateId, string? CityId, string? Street, string? Landmark,
       double? Latitude, double? Longitude, string? ClientName, string? Mobile, bool? IsDefualt, int PageIndex = 1, int PageSize = 100)
       : IRequestBase<PagingViewModel<GetAllShippingAddressesDTO>>;
    public class GetAllShippingAddressesQueryHandler : RequestHandlerBase<ShippingAddress, GetAllShippingAddressesQuery, PagingViewModel<GetAllShippingAddressesDTO>>
    {
        public GetAllShippingAddressesQueryHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<PagingViewModel<GetAllShippingAddressesDTO>>> Handle(GetAllShippingAddressesQuery request, CancellationToken cancellationToken)
        {
        
            var predicate = PredicateExtensions.PredicateExtensions.Begin<ShippingAddress>(true)
                .And(c => string.IsNullOrEmpty(request.GovernorateId) || c.GovernorateId == request.GovernorateId)
                .And(c => string.IsNullOrEmpty(request.CityId) || c.CityId == request.CityId)
                .And(c => string.IsNullOrEmpty(request.Street) || c.Street.Contains(request.Street))
                .And(c => string.IsNullOrEmpty(request.Landmark) || c.Landmark.Contains(request.Landmark))
                .And(c => request.Latitude == null || c.Latitude == request.Latitude)
                .And(c => request.Longitude == null || c.Longitude == request.Longitude)
                .And(c => request.IsDefualt == null || c.IsDefualt == request.IsDefualt);

            var query = _repository.Get(predicate);

            var paginatedResult = await query
                .ToPagesAsync(request.PageIndex, request.PageSize);

            var resultList = new List<GetAllShippingAddressesDTO>();

            foreach (var shippingAddress in paginatedResult.Items)
            {
                var clientName = string.Empty;
                var clientMobile = string.Empty;

                if (!string.IsNullOrEmpty(shippingAddress.ClientId))
                {
                    var client = await _mediator.Send(new GetClientByIdQuery(shippingAddress.ClientId));
                    clientName = client?.Data.Name ?? "No Client";
                    clientMobile = client?.Data.Mobile ?? "No Mobile";
                }

                if (!string.IsNullOrEmpty(request.ClientName) && !clientName.Contains(request.ClientName)) continue;
                if (!string.IsNullOrEmpty(request.Mobile) && !clientMobile.Contains(request.Mobile)) continue;

                var cityName = !string.IsNullOrEmpty(shippingAddress.CityId)
                    ? (await _mediator.Send(new GetCityByIDQuery(shippingAddress.CityId)))?.Data.Name ?? string.Empty
                    : "No City";

                var governorateName = !string.IsNullOrEmpty(shippingAddress.GovernorateId)
                    ? (await _mediator.Send(new GetGovernorateByIDQuery(shippingAddress.GovernorateId)))?.Data.Name ?? string.Empty
                    : "No Governorate";

                resultList.Add(new GetAllShippingAddressesDTO(
                    GovernorateName: governorateName,
                    GovernorateId: shippingAddress.GovernorateId,
                    CityName: cityName,
                    CityId: shippingAddress.CityId,
                    Street: shippingAddress.Street,
                    Landmark: shippingAddress.Landmark,
                    Latitude: shippingAddress.Latitude,
                    Longitude: shippingAddress.Longitude,
                    ClientName: clientName,
                    ClientId: shippingAddress.ClientId,
                    IsDefualt: shippingAddress.IsDefualt
                    ));
            }

           
            var result = new PagingViewModel<GetAllShippingAddressesDTO>
            {
                PageIndex = paginatedResult.PageIndex,
                PageSize = paginatedResult.PageSize,
                Records = paginatedResult.Records,
                Pages = paginatedResult.Pages,
                Items = resultList
            };

            return RequestResult<PagingViewModel<GetAllShippingAddressesDTO>>.Success(result);
        }


    }
}
