using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Users.ResetPassword.Commands;

namespace KOG.ECommerce.Features.Users.ResetPassword
{
    public record ResetPasswordRequest(string UserID ,string Password,string ConfirmPassword);
    public class ResetPasswordRequestValidtor : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidtor()
        {
        }
    }

    public class ResetPasswordRequestProfile : Profile
    {
        public ResetPasswordRequestProfile()
        {
            CreateMap<ResetPasswordRequest, ResetPasswordCommand>();
        }
    }
}
