using AutoMapper;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.DTOs
{
    public record OTPDTO(string OTP,string OTPtoken);
    public class OTPProfile : Profile
    {
        public OTPProfile() 
        {
            CreateMap<User, OTPDTO>();
        }
    }
}
