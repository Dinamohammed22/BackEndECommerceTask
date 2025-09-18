using KOG.ECommerce.Common.DTOs;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Users.ApproveUser.Commands
{
    public record ApproveUserCommand(string ID):IRequestBase<bool>;
    public class ApproveUserCommandHandler : RequestHandlerBase<User, ApproveUserCommand, bool>
    {
        public ApproveUserCommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ApproveUserCommand request, CancellationToken cancellationToken)
        {
            var user =  _repository.GetByID(request.ID);

            if (user != null) {
                if (user.VerifyStatus == VerifyStatus.Verified)
                {
                    user.VerifyStatus = VerifyStatus.Approve;
                    _repository.SaveIncluded(user, nameof(user.VerifyStatus));
                    _repository.SaveChanges();
                    //var sms = await SMSHelper.SendSmsAsync(user.Mobile, "Your account has been approved. Welcome aboard!");
                    //if (!sms.Success)
                    //    return RequestResult<bool>.Failure(ErrorCode.CannotSend);
                    return RequestResult<bool>.Success(true);
                }
                else
                {
                    return RequestResult<bool>.Failure(ErrorCode.NotVerified);
                }
            }
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        }
    }


}
