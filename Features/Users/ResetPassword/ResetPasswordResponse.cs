using AutoMapper;
using KOG.ECommerce.Features.Users.Login;

namespace KOG.ECommerce.Features.Users.ResetPassword
{
    public record ResetPasswordResponse(string Token);
    public class ResetPasswordResponseProfile : Profile
    {
        public ResetPasswordResponseProfile()
        {
            CreateMap<string, ResetPasswordResponse>();
        }
    }
}
