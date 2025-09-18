using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Users.ActivateUser.Commands;

namespace KOG.ECommerce.Features.Users.ActivateUser
{
    public record ActivateUserRequestViewModel(string ID);
    public class ActivateUserRequestValidator:AbstractValidator<ActivateUserRequestViewModel>
    {
        public ActivateUserRequestValidator() { }
    }
    public class ActivateUserRequestProfile : Profile
    {
        public ActivateUserRequestProfile()
        {
            CreateMap<ActivateUserRequestViewModel, ActivateUserCommand>();
        }
    }
}
