using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public record GetClientViewByIdQuery(string ID) : IRequestBase<GetClientViewByIdDTO>;
    public class GetClientViewByIdQueryHandler : RequestHandlerBase<Client, GetClientViewByIdQuery, GetClientViewByIdDTO>
    {
        public GetClientViewByIdQueryHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<GetClientViewByIdDTO>> Handle(GetClientViewByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetWithDeleted().Where(c => c.ID == request.ID)
                .Include(c => c.ShippingAddresses).ThenInclude(s => s.Governorate)
                .Include(c => c.ShippingAddresses).ThenInclude(s => s.City)
                .Include(c => c.User)
                .Include(c => c.Orders)
                .Include(c => c.ClientGroup)
                .FirstOrDefaultAsync(cancellationToken);

            if (client == null)
            {
                return RequestResult<GetClientViewByIdDTO>.Failure(ErrorCode.NotFound);
            }

            var model = client.MapOne<GetClientViewByIdDTO>();
            model.NumberOfOrder = client.Orders.Count;
            return RequestResult<GetClientViewByIdDTO>.Success(model);
        }

    }
}
