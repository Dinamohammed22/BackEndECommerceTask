using AutoMapper;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Users.GetAllUsers
{
    public record GetAllUsersResponseViewModel(string Id, string Name, string Mobile, string? JobTitle, Role RoleId,bool IsActive);
    public class GetAllUsersResponseProfile : Profile
    {
        public GetAllUsersResponseProfile()
        {
            CreateMap<GetAllUsersDTO,GetAllUsersResponseViewModel >();
        }
    }
}
