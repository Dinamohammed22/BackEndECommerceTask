using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Users.RejectUser.Commands;

namespace KOG.ECommerce.Features.Users.RejectUser
{
    public record RejectUserRequestViewModel(string ID, string? RejectReason);
    public class RejectUserRequestValidator : AbstractValidator<RejectUserRequestViewModel>
    {
        public RejectUserRequestValidator() { }
    }
    public class RejectUserRequestProfile : Profile
    {
        public RejectUserRequestProfile()
        {
            CreateMap<RejectUserRequestViewModel, RejectUserCommand>();
        }
    }
}
