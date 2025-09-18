using AutoMapper;

namespace KOG.ECommerce.Features.Users.ResendOTP
{
    public record ResendOTPResponseViewModel(string OTPtoken);
    public class ResendOTPResponseProfile : Profile
    {
        public ResendOTPResponseProfile()
        {
            CreateMap<string, ResendOTPResponseViewModel>()
            .ConstructUsing(otpToken => new ResendOTPResponseViewModel(otpToken));
        }
    }
}
