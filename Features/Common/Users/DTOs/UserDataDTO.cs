using AutoMapper;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.DTOs
{
    public record UserDataDTO(string ID , string Name,string Phone,string? Email,string? Path);
    public class UserDataDTOProfile : Profile
    {
        public UserDataDTOProfile()
        {
            CreateMap<User, UserDataDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Mobile))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src =>
                    // Check role and return email accordingly
                    src.RoleId == Role.Client && src.Client != null ? src.Client.Email :
                    src.RoleId == Role.Company && src.Company != null ? src.Company.Email :
                    null)); // Fallback in case no valid email found
        }
    }


}
