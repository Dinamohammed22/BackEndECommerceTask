using AutoMapper;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.DTOs
{
    public record GetUserByIDDTO(string Id, string Name, string Mobile, string JobTitle);
    public class GetUserByIDDTOProfile : Profile
    {
        public GetUserByIDDTOProfile()
        {
            CreateMap<User, GetUserByIDDTO>();
            CreateMap<GetUserByIDDTO, GetUserByIDQuery>();
        }
    }
}
