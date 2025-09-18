using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Clients.ChangePassword.Commands;
using KOG.ECommerce.Features.Common.Users.ChangeUserPassword.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.Clients.ChangePassword.Orchestrators
{
    public record ChangePasswordOrchestrator(string Password, string ConfirmPassword, string? ID) : IRequestBase<bool>;
    public class ChangePasswordOrchestratorHandler : RequestHandlerBase<Client, ChangePasswordOrchestrator, bool>
    {
        public ChangePasswordOrchestratorHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ChangePasswordOrchestrator request, CancellationToken cancellationToken)
        {
            var ID = request.ID;
            if (ID == null)
                ID = _userState.UserID;
            var password = PasswordHasher.Hash(request.Password);
            var UserResult= await _mediator.Send(new ChangeUserPasswordCommand(request.Password, request.ConfirmPassword, ID));
            if (!UserResult.IsSuccess)
                return RequestResult<bool>.Failure(UserResult.ErrorCode);
            var result = await _mediator.Send(new ChangePasswordCommand(password, ID));
            if (!result.IsSuccess)
                return RequestResult<bool>.Failure(result.ErrorCode);
            return RequestResult<bool>.Success(true);
        }
    }


}
