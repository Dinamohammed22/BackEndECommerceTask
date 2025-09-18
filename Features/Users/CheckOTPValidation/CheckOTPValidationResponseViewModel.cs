using AutoMapper;

namespace KOG.ECommerce.Features.Users.CheckOTPValidation
{
    public record CheckOTPValidationResponseViewModel(string UserID);
    public class CheckOTPValidationResponseProfile : Profile
    {
        public CheckOTPValidationResponseProfile()
        {
            CreateMap<string, CheckOTPValidationResponseViewModel>().ConstructUsing(userId => new CheckOTPValidationResponseViewModel(userId));
        }
    }
}
