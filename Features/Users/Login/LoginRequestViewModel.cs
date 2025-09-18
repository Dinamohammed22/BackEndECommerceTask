using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Features.Users.Login.Commands;
using KOG.ECommerce.Features.Users.Login.Orchestrators;

namespace KOG.ECommerce.Features.Users.Login
{
    public record LoginRequestViewModel(string Token, string OTP, string? FirebaseToken);
    public class LoginRequestVaildator : AbstractValidator<LoginRequestViewModel>
    {
        public LoginRequestVaildator() {

        }
    }
    public class LoginRequestEndPoint : Profile
    {
        public LoginRequestEndPoint() {
            CreateMap<LoginRequestViewModel, LoginOrchestrator>();
            CreateMap<LoginOrchestrator, CheckOTPValidationQuery>();

        }
    }
}
