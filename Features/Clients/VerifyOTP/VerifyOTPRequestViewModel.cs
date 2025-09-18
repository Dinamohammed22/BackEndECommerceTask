using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Clients.ClientRegister.Commands;
using KOG.ECommerce.Features.Clients.ClientRegister;
using KOG.ECommerce.Features.Clients.VerifyOTP.Commands;

namespace KOG.ECommerce.Features.Clients.VerifyOTP
{
    public record VerifyOTPRequestViewModel(string Token, string Otp);
    public class VerifyOTPRequestValidation : AbstractValidator<VerifyOTPRequestViewModel>
    {
        public VerifyOTPRequestValidation() { }
    }
    public class VerifyOTPEndPointRequestProfile : Profile
    {
        public VerifyOTPEndPointRequestProfile()
        {
            CreateMap<VerifyOTPRequestViewModel, VerifyOTPCommand>();
        }
    }
}
