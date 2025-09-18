using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Models.Clients;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public record GetClientsWithDefualtShippingAddressQuery (): IRequestBase<IEnumerable<GetClientsWithDefualtShippingAddressDTO>>;

    public class GetClientsWithDefualtShippingAddressQueryHandler : RequestHandlerBase<Client, GetClientsWithDefualtShippingAddressQuery, IEnumerable<GetClientsWithDefualtShippingAddressDTO>>
    {
        public GetClientsWithDefualtShippingAddressQueryHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<IEnumerable<GetClientsWithDefualtShippingAddressDTO>>> Handle(GetClientsWithDefualtShippingAddressQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.Get()
                .Include(c=>c.User)
                .Include(c => c.ShippingAddresses)
                    .ThenInclude(sa => sa.City)
                    .ThenInclude(sa => sa.Governorate);

            var result = query.Select(client => new GetClientsWithDefualtShippingAddressDTO(
                client.NationalNumber,
                client.Name,
                client.Mobile,
                client.ShippingAddresses
                    .Where(sa => sa.IsDefualt)
                    .Select(sa => new GetAllShippingAddressesDTO(
                        sa.Governorate.Name,
                        sa.Governorate.ID.ToString(),
                        sa.City.Name,
                        sa.City.ID.ToString(),
                        sa.Street,
                        sa.Landmark,
                        sa.Latitude,
                        sa.Longitude,
                        client.Name,
                        client.ID.ToString(),
                        sa.IsDefualt
                    )).FirstOrDefault(),
                client.User.IsActive,
                client.ID
            ));

            return RequestResult<IEnumerable<GetClientsWithDefualtShippingAddressDTO>>.Success(result);
        }


    }




}