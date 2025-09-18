using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Clients.ClientRegister.Commands;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Clients.VerifyOTP.Commands
{
    public record VerifyOTPCommand( string Token , string OTP ):IRequestBase<bool>;
    public class VerifyOtpCommandHandler : RequestHandlerBase<User, VerifyOTPCommand, bool>
    {
        public VerifyOtpCommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(u => u.OTP == request.OTP && u.OTPtoken == request.Token).FirstOrDefaultAsync();
            if (user != null)
            {
                if (user.OTPExpiration >= DateTime.Now)
                {
                    user.VerifyStatus = VerifyStatus.Verified;
                    _repository.SaveIncluded(user,nameof(user.VerifyStatus));
                    _repository.SaveChanges();
                    return RequestResult<bool>.Success(true);
                }
            }
            return RequestResult<bool>.Failure(ErrorCode.InvalidOTP);

        }
    }


}
