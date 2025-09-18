using AutoMapper;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Users.Login
{
    public record LoginResponseViewModel(string Token, Role RoleId);

    public class LoginResponseProfile : Profile
    {
        public LoginResponseProfile()
        {
            CreateMap<LoginDTO, LoginResponseViewModel>();
        //        .ConstructUsing(token => new LoginResponseViewModel(token, Role.None)); // Use a default or derived Role value
        }
    }
}
