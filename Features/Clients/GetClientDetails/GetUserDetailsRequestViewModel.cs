using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Clients.Queries;

namespace KOG.ECommerce.Features.Clients.GetUserDetails
{
    public record GetUserDetailsRequestViewModel(string ID);
    public class GetUserDetailsRequestValidator : AbstractValidator<GetUserDetailsRequestViewModel>
    {
        public GetUserDetailsRequestValidator() { }
    }
    public class GetUserDetailsRequestProfile : Profile
    {
        public GetUserDetailsRequestProfile()
        {
            CreateMap<GetUserDetailsRequestViewModel, GetclientDetailsQuery>();
        }
    }
}
