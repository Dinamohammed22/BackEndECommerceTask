using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Users.DeactivateUser.Commands;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Users.BulkDeActivateUser.Orchestrator
{
    public record BulkDeActivateUserOrchestrator(List<string> IDs) : IRequestBase<bool>;
    public class BulkDeActivateUserOrchestratorHandler : RequestHandlerBase<User, BulkDeActivateUserOrchestrator, bool>
    {
        public BulkDeActivateUserOrchestratorHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<RequestResult<bool>> Handle(BulkDeActivateUserOrchestrator request, CancellationToken cancellationToken)
        {
            foreach (var ID in request.IDs)
            {
                var check = await _mediator.Send(new DeactivateUserCommand(ID));
                if (!check.IsSuccess)
                {
                    return RequestResult<bool>.Failure(check.ErrorCode);
                }
            }

            return RequestResult<bool>.Success(true);
        }
    }
}
