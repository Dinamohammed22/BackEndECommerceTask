using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Users.RejectUser.Commands
{
    public record RejectUserCommand(string ID, string? RejectReason) : IRequestBase<bool>;
    public class RejectUserCommandHandler : RequestHandlerBase<User, RejectUserCommand, bool>
    {
        public RejectUserCommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(RejectUserCommand request, CancellationToken cancellationToken)
        {
            var user =await _repository.GetByIDAsync(request.ID);
            if (user != null)
            {
                user.VerifyStatus = VerifyStatus.Reject;
                _repository.SaveIncluded(user, nameof(user.VerifyStatus));
                _repository.SaveChanges();
                if (!request.RejectReason.IsNullOrEmpty())
                {
                    //string Message = $"We regret to inform you that your data could not be updated. Reason: {request.RejectReason}.";
                    //var smsResponse = await SMSHelper.SendSmsAsync(user.Mobile, Message);
                    //if (!smsResponse.Success)
                    //    return RequestResult<bool>.Failure(ErrorCode.CannotSend);
                }
                return RequestResult<bool>.Success(true);
            }
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        }
    }
}
