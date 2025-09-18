using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Users.EditUser.Commands;

namespace KOG.ECommerce.Features.Users.EditClient
{
    public record EditUserRequestViewModel(string Id, string Name, string Mobile, string? JobTitle);
    public class EditUserRequestValidator:AbstractValidator<EditUserRequestViewModel>
    {
        public EditUserRequestValidator() { }
    }
    public class EditUserRequestProfile : Profile
    {
        public EditUserRequestProfile()
        {
            CreateMap<EditUserRequestViewModel, EditUserCommand>();
        }
    }
}
