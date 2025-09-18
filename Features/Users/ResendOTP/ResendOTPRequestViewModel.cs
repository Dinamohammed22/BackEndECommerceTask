using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Features.Users.ResendOTP.Orchestrators;

namespace KOG.ECommerce.Features.Users.ResendOTP
{
    public record ResendOTPRequestViewModel(string Token);
    public class ResendOTPRequestValidator:AbstractValidator<ResendOTPRequestViewModel>
    {
        public ResendOTPRequestValidator()
        {

        }
    }
    public class ResendOTPRequestProfile:Profile
    {
        public ResendOTPRequestProfile()
        {
            CreateMap<ResendOTPOrchestrator, CheckTOResendOTPQuery>();
            CreateMap<ResendOTPRequestViewModel, ResendOTPOrchestrator>();

        }
    }
}
