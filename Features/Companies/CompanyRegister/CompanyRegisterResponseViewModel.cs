using AutoMapper;

namespace KOG.ECommerce.Features.Companies.CompanyRegister
{
    public record CompanyRegisterResponseViewModel(string OTPtoken);
    public class CompanyRegisterResponseProfile:Profile
    {
        public CompanyRegisterResponseProfile()
        {
            CreateMap<string, CompanyRegisterResponseViewModel>()
            .ConstructUsing(otpToken => new CompanyRegisterResponseViewModel(otpToken));
        }
    }
}
