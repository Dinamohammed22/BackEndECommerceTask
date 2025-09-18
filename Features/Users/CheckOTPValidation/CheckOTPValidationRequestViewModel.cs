using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Features.Users.Login.Orchestrators;

namespace KOG.ECommerce.Features.Users.CheckOTPValidation
{
    public record CheckOTPValidationRequestViewModel(string Token, string OTP);
    public class CheckOTPValidationRequestVaildator : AbstractValidator<CheckOTPValidationRequestViewModel>
    {
        public CheckOTPValidationRequestVaildator()
        {

        }
    }
    public class CheckOTPValidationRequestProfile : Profile
    {
        public CheckOTPValidationRequestProfile()
        {
            CreateMap<CheckOTPValidationRequestViewModel, CheckOTPValidationQuery>();
            //CreateMap<LoginOrchestrator, CheckOTPValidationQuery>();

        }
    }
}
