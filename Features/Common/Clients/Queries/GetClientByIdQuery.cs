using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public record GetClientByIdQuery(string ID):IRequestBase<GetClientByIdDTO>;
    public class GetListClientsQueryHandler : RequestHandlerBase<Client, GetClientByIdQuery, GetClientByIdDTO>
    {
        public GetListClientsQueryHandler(RequestHandlerBaseParameters<Client> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<GetClientByIdDTO>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {

            var client = await _repository.Get(c => c.ID == request.ID)
                .Include(c => c.ClientGroup)
                .Include(c => c.ShippingAddresses)
                .FirstOrDefaultAsync();

            // Return an error if the client is not found
            if (client == null)
                return RequestResult<GetClientByIdDTO>.Failure();

            // Map the client entity to the DTO
            var clientDto = client.MapOne<GetClientByIdDTO>();

            return RequestResult<GetClientByIdDTO>.Success(clientDto);
        }
    }

}
