using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Features.ShippingAddresses.ApproveShippingAddress.Commands;
using KOG.ECommerce.Features.Users.ActivateUser.Commands;
using KOG.ECommerce.Features.Users.ApproveUser.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Users.ApproveUser.Orchestrators
{
    public record ApproveUserOrchestrator(string ID) : IRequestBase<bool>;
    public class ApproveUserOrchestratorHandler : RequestHandlerBase<User, ApproveUserOrchestrator, bool>
    {
        public ApproveUserOrchestratorHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ApproveUserOrchestrator request, CancellationToken cancellationToken)
        {
            var activeClient = await _mediator.Send(request.MapOne<ActivateUserCommand>());
            if (!activeClient.IsSuccess) {
                return RequestResult<bool>.Failure(activeClient.ErrorCode);
            }
            var approve = await _mediator.Send(request.MapOne<ApproveUserCommand>());
            if (!approve.IsSuccess)
            {
                return RequestResult<bool>.Failure(approve.ErrorCode);
            }
            var Role=_repository.GetByID(request.ID).RoleId;
            if (Role == Role.Client)
            {
                var tempShippingAddressId = await _mediator.Send(new GetIDOfPendingShippingAddressQuery(request.ID));
                var shippingAddress = await _mediator.Send(new ApproveShippingAddressCommand(tempShippingAddressId.Data));
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
