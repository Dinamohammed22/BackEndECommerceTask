using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.ClientGroups.Queries;
using KOG.ECommerce.Models.ClientGroups;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.ClientGroups.BulkDeleteClientGroups.Commands
{
    public record BulkDeleteClientGroupsCommand(List<string> Ids):IRequestBase<bool>;
    public class BulkDeleteClientGroupsCommandHandler : RequestHandlerBase<ClientGroup, BulkDeleteClientGroupsCommand, bool>
    {
        public BulkDeleteClientGroupsCommandHandler(RequestHandlerBaseParameters<ClientGroup> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkDeleteClientGroupsCommand request, CancellationToken cancellationToken)
        {
            var existingIds = await _repository.Get()
                                               .Where(b => request.Ids.Contains(b.ID))
                                               .Select(b => b.ID)
                                               .ToListAsync(cancellationToken);

            if (existingIds.Count != request.Ids.Count)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            foreach (var id in request.Ids)
            {
                var hasCompany = await _mediator.Send(new CheckClientGroupHasClientQuery(id));
                if (hasCompany.Data)
                    return RequestResult<bool>.Failure(ErrorCode.CannotDelete);

                _repository.Delete(id);
            }

            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);

        }
    }
}
