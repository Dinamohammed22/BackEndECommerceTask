using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.ClientGroups.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.ClientGroups;

namespace KOG.ECommerce.Features.ClientGroups.DeleteClientGroup.Commands
{
    public record DeleteClientGroupCommand(string Id) : IRequestBase<bool>;
    public class DeleteClientGroupCommandHandler : RequestHandlerBase<ClientGroup, DeleteClientGroupCommand, bool>
    {
        public DeleteClientGroupCommandHandler(RequestHandlerBaseParameters<ClientGroup> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(DeleteClientGroupCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.Id);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            var HasCompany = await _mediator.Send(request.MapOne<CheckClientGroupHasClientQuery>());
            if (!HasCompany.Data)
            {
                _repository.Delete(request.Id);
                _repository.SaveChanges();
                return await Task.FromResult(RequestResult<bool>.Success(true));
            }

            return RequestResult<bool>.Failure(ErrorCode.CannotDelete);
        }
    }
}
