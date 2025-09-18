using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.Common.ClientGroups.Queries
{
    public record CheckClientGroupHasClientQuery(string ID) : IRequestBase<bool>;
    public class CheckClientGroupHasClientQueryHandler : RequestHandlerBase<Client, CheckClientGroupHasClientQuery, bool>
    {
        public CheckClientGroupHasClientQueryHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckClientGroupHasClientQuery request, CancellationToken cancellationToken)
        {
            var findClientGroupId = await _repository.AnyAsync(c => c.ClientGroupId == request.ID);
            return RequestResult<bool>.Success(findClientGroupId);
        }
    }
}
