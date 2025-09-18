using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Features.Users.OTPLogin.Orchestrators;

namespace KOG.ECommerce.Features.Users.OTPLogin
{
    public record OTPLoginRequestViewModel(string Mobile, string Password);
    public class OTPLoginRequestValidator : AbstractValidator<OTPLoginRequestViewModel>
    {
        public OTPLoginRequestValidator() { }
    }
    public class OTPLoginRequestProfile : Profile
    {
        public OTPLoginRequestProfile()
        {
            CreateMap<OTPLoginRequestViewModel, OTPLoginOrchestrator>();
          

        }
    }
}
