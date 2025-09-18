using AutoMapper;

namespace KOG.ECommerce.Features.Users.ResetPasswordOTP
{
    public record ResetPasswordOTPResponseViewModel(string OTPtoken);
    public class ResetPasswordOTPResponseProfile : Profile
    {
        public ResetPasswordOTPResponseProfile()
        {
            CreateMap<string, ResetPasswordOTPResponseViewModel>()
            .ConstructUsing(otpToken => new ResetPasswordOTPResponseViewModel(otpToken));
        }
    }
}
