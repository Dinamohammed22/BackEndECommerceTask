using AutoMapper;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.DTOs
{
    public record LoginDTO(string Token,Role RoleId);
    public class LoginDTOProfile : Profile
    {
        public LoginDTOProfile() {
            CreateMap<User, LoginDTO>();
        }
    }
}
