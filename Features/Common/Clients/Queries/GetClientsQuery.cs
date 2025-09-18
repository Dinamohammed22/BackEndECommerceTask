using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using Microsoft.EntityFrameworkCore;


namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public class GetClientsQuery : IRequestBase<IEnumerable<ClientProfileDTO>>
    {
        public class GetListClientsQueryHandler : RequestHandlerBase<Client, GetClientsQuery, IEnumerable<ClientProfileDTO>>
        {
            public GetListClientsQueryHandler(RequestHandlerBaseParameters<Client> parameters) : base(parameters)
            {
            }

            public override async Task<RequestResult<IEnumerable<ClientProfileDTO>>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
            {
                var query = _repository.Get()
                    .Include(c => c.ShippingAddresses)
                    .ThenInclude(sa => sa.City)
                    .ThenInclude(sa => sa.Governorate).MapList<ClientProfileDTO>();
                   
                return RequestResult<IEnumerable<ClientProfileDTO>>.Success(query);
            }
        }
    }
}
