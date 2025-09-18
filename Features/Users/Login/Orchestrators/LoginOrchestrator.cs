using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Features.Users.Login.Commands;
using KOG.ECommerce.Features.Users.UpdateFirebaseToken.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Users.Login.Orchestrators
{
    public record LoginOrchestrator(string Token, string OTP, string? FirebaseToken) : IRequestBase<LoginDTO>;
    public class LoginOrchestratorHandler : RequestHandlerBase<User, LoginOrchestrator, LoginDTO>
    {
        public LoginOrchestratorHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<LoginDTO>> Handle(LoginOrchestrator request, CancellationToken cancellationToken)
        {
            var userID = await _mediator.Send(request.MapOne<CheckOTPValidationQuery>());
            if (!userID.IsSuccess) {
                return RequestResult<LoginDTO>.Failure(userID.ErrorCode);
            }
            var isApproved = await _repository.AnyAsync(c => c.VerifyStatus == VerifyStatus.Approve && c.ID == userID.Data);
            if (!isApproved)
            {
                return RequestResult<LoginDTO>.Failure(ErrorCode.NotApproved);
            }

            var isActiveClient = await _mediator.Send(new CheckUserActivationQuery(userID.Data));
            if (!isActiveClient.Data)
            {
                return RequestResult<LoginDTO>.Failure(ErrorCode.NotActive);
            }
            if (!userID.Data.IsNullOrEmpty())
            {
                var token = await _mediator.Send(new LoginCommand(userID.Data));
                var result = RequestResult<LoginDTO>.Success(token.Data);
                var FirebaseToken = await _mediator.Send(new UpdateFirebaseTokenCommand(userID.Data, request.FirebaseToken));
                if (!FirebaseToken.IsSuccess)
                {
                    return RequestResult<LoginDTO>.Failure(FirebaseToken.ErrorCode);
                }
                return await Task.FromResult(result);
            }
            return RequestResult<LoginDTO>.Failure(userID.ErrorCode);
        }
    }

}
