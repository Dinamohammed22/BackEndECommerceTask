using AutoMapper;
using KOG.ECommerce.Features.Common.Users.DTOs;

namespace KOG.ECommerce.Features.Users.GetUserByID
{
    public record GetUserByIDResponseViewModel(string Id, string Name, string Mobile, string JobTitle);
    public class GetUserByIDResponseProfile : Profile
    {
        public GetUserByIDResponseProfile()
        {
            CreateMap<GetUserByIDDTO, GetUserByIDResponseViewModel>();
        }
    }
}
