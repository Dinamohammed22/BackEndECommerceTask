using AutoMapper;
using KOG.ECommerce.Features.Clients.ClientRegister;

namespace KOG.ECommerce.Features.Users.OTPLogin
{
    public record OTPLoginResponseViewModel(string OTPtoken);
    public class OTPLoginResponseProfile : Profile
    {
        public OTPLoginResponseProfile()
        {
            CreateMap<string, OTPLoginResponseViewModel>()
            .ConstructUsing(otpToken => new OTPLoginResponseViewModel(otpToken));
        }
    }
}
