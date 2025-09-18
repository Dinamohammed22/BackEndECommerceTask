using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Users.OTPLogin.Orchestrators;
using KOG.ECommerce.Features.Users.ResetPasswordOTP.Orchestrators;

namespace KOG.ECommerce.Features.Users.ResetPasswordOTP
{
    public record ResetPasswordOTPRequestViewModel(string Mobile);
    public class ResetPasswordOTPRequestValidator : AbstractValidator<ResetPasswordOTPRequestViewModel>
    {
        public ResetPasswordOTPRequestValidator() { }
    }
    public class OTPLoginRequestProfile : Profile
    {
        public OTPLoginRequestProfile()
        {
            CreateMap<ResetPasswordOTPRequestViewModel, ResetPasswordOTPOrchestrator>();
        }
    }
}
