using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Advertisements.DTOs;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public record GetclientDetailsQuery(string ID):IRequestBase<GetClientDetailsDTO>;
    public class GetclientDetailsQueryHandler : RequestHandlerBase<Client, GetclientDetailsQuery, GetClientDetailsDTO>
    {
        public GetclientDetailsQueryHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetClientDetailsDTO>> Handle(GetclientDetailsQuery request, CancellationToken cancellationToken)
        {
            var client = await _repository.Get(c => c.ID == request.ID)
              .Include(c => c.User)
              .FirstOrDefaultAsync();
            if (client == null)
            {
                return RequestResult<GetClientDetailsDTO>.Failure(ErrorCode.NotFound);
            }
            var Address=await _mediator.Send(new GetDefualtShippingAddressForClientQuery(request.ID));
            if(!Address.IsSuccess)
            {
                return RequestResult<GetClientDetailsDTO>.Failure(Address.ErrorCode);
            }

          var clientDetails = new GetClientDetailsDTO(
          NationalNumber: client.NationalNumber,
          Name: client.Name,
          Password: client.User.Password,
          Mobile: client.Mobile,
          GovernorateId: Address.Data.GovernorateId,
          CityId: Address.Data.CityId,
          GovernorateName:Address.Data.GovernorateName,
          CityName: Address.Data.CityName,
          Street: Address.Data.Street,
          Landmark: Address.Data.Landmark,
          Latitude: Address.Data.Latitude,
          Longitude: Address.Data.Longitude,
          Email: client.Email,
          Phone: client.Phone,
          Status:Address.Data.Status,
          ClientActivity: client.ClientActivity,
          BuildingData: Address.Data.BuildingData,
          Path: string.Empty,
          Religion:client.Religion);
            var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(request.ID, SourceType.Client));

            var clienttWithMedia = clientDetails with
            {
                Path = mediaResult.IsSuccess ? mediaResult.Data : string.Empty
            };
            return RequestResult<GetClientDetailsDTO>.Success(clienttWithMedia);
        }
    }
}
