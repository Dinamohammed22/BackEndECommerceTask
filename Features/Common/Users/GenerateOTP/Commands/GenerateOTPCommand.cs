using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.GenerateOTP.Commands
{
    public record GenerateOTPCommand(string UserId,string Mobile):IRequestBase<OTPDTO>;
    public class GenerateOTPCommandHandler : RequestHandlerBase<User, GenerateOTPCommand, OTPDTO>
    {
        public GenerateOTPCommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<OTPDTO>> Handle(GenerateOTPCommand request, CancellationToken cancellationToken)
        {
            var role=_repository.GetByID(request.UserId).RoleId;
            //string otp = GenerateOtp(6);
            DateTime expirationDate = DateTime.Now.AddMinutes(15);
            var token = TokenGenerator.Generate(request.UserId, request.Mobile,role);
            User user=new User { ID=request.UserId};
            //user.OTP = otp;
            user.OTP = "112233";
            user.OTPtoken = token;
            user.OTPExpiration=expirationDate;
            _repository.SaveIncluded(user,nameof(user.OTP),nameof(user.OTPtoken),nameof(user.OTPExpiration));
            _repository.SaveChanges();
            OTPDTO oTPDTO = new OTPDTO(user.OTP, user.OTPtoken);
            return RequestResult<OTPDTO>.Success(oTPDTO);
        }
        private string GenerateOtp(int length)
        {
            Random random = new Random();
            return random.Next(0, (int)Math.Pow(10, length)).ToString("D" + length);
        }
    }
}
