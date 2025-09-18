using AutoMapper;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.DTOs
{
    public record GetAllUsersDTO(string Id, string Name, string Mobile, string? JobTitle,Role RoleId,bool IsActive);
    public class GetAllUsersProfile : Profile
    {
        public GetAllUsersProfile()
        {
            CreateMap<User, GetAllUsersDTO>();
        }
    }
}
