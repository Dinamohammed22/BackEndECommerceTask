using AutoMapper;

namespace KOG.ECommerce.Features.Clients.ClientRegister
{
    public record ClientRegisterResponseViewModel(string OTPtoken);
    public class ClientRegisterResponseProfile : Profile
    {
        public ClientRegisterResponseProfile()
        {
            CreateMap<string, ClientRegisterResponseViewModel>()
            .ConstructUsing(otpToken => new ClientRegisterResponseViewModel(otpToken));
        }
    }
}
