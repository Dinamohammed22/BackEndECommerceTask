using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public record GetClientsByGroupIDQuery(string CompanyGroupId) : IRequestBase<IEnumerable<GetClientByGroupIDProfileDTO>>;
    public class GetClientsByGroupIDQueryHandler : RequestHandlerBase<Client, GetClientsByGroupIDQuery, IEnumerable<GetClientByGroupIDProfileDTO>>
    {
        public GetClientsByGroupIDQueryHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetClientByGroupIDProfileDTO>>> Handle(GetClientsByGroupIDQuery request, CancellationToken cancellationToken)
        {
            var companies = _repository.Get(c => c.ClientGroupId == request.CompanyGroupId).MapList<GetClientByGroupIDProfileDTO>();
            return RequestResult<IEnumerable<GetClientByGroupIDProfileDTO>>.Success(companies);

        }
    }
}
