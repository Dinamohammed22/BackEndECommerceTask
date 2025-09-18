using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.ClientGroups.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.ClientGroups;

namespace KOG.ECommerce.Features.Common.ClientGroups.Queries
{
    public record GetClientGroupByIDQuery(string Id):IRequestBase<ClientGroupProfileDTO>;
    public class GetClientGroupByIDQueryHandler : RequestHandlerBase<ClientGroup, GetClientGroupByIDQuery, ClientGroupProfileDTO>
    {
        public GetClientGroupByIDQueryHandler(RequestHandlerBaseParameters<ClientGroup> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<ClientGroupProfileDTO>> Handle(GetClientGroupByIDQuery request, CancellationToken cancellationToken)
        {
            var clientGroup=_repository.GetByID(request.Id).MapOne<ClientGroupProfileDTO>();
            if (clientGroup == null) { 
                return RequestResult<ClientGroupProfileDTO>.Failure(ErrorCode.NotFound);
            }
            return RequestResult<ClientGroupProfileDTO>.Success(clientGroup);
        }
    }
}
