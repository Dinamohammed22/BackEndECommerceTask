using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Users.CreateUser.Commands;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Users.CreateUser
{
    public record CreateUserRequestViewModel(string Name, string Password, string ConfirmPassword, string Mobile, Role RoleId,
         string? JobTitle, VerifyStatus VerifyStatus = VerifyStatus.Approve);
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequestViewModel>
    {
        public CreateUserRequestValidator()
        {

        }
    }
    public class CreateUserRequestProfile : Profile
    {
        public CreateUserRequestProfile()
        {
            CreateMap<CreateUserRequestViewModel, CreateUserCommand>();
        }
    }
}
