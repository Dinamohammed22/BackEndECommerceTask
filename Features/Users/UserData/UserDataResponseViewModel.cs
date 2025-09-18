using AutoMapper;
using KOG.ECommerce.Features.Common.Users.DTOs;

namespace KOG.ECommerce.Features.Users.UserData
{
    public record UserDataResponseViewModel(string ID ,string Name, string Phone, string Email, string? Path);
    public class UserDataResponseProfile : Profile
    {
        public UserDataResponseProfile() {
            CreateMap<UserDataDTO, UserDataResponseViewModel>();
        }
    }
}
