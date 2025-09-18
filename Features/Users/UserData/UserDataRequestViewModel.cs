using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Users.Queries;

namespace KOG.ECommerce.Features.Users.UserData
{
    public record UserDataRequestViewModel();
    public class UserDataRequestValidator : AbstractValidator<UserDataRequestViewModel>
    {
        public UserDataRequestValidator() { }
    }
    public class UserDataRequestProfile : Profile
    {
        public UserDataRequestProfile() {
            CreateMap<UserDataRequestViewModel, UserDataQuery>();
        }
    }
}
