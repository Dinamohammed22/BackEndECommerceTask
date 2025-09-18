using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Users.Queries;

namespace KOG.ECommerce.Features.Users.GetUserByID
{
    public record GetUserByIDRequestViewModel(string ID);
    public class GetUserByIDRequestValidator : AbstractValidator<GetUserByIDRequestViewModel>
    {
        public GetUserByIDRequestValidator()
        {
        }
    }
    public class GetUserByIDRequestProfile : Profile
    {
        public GetUserByIDRequestProfile()
        {
            CreateMap<GetUserByIDRequestViewModel, GetUserByIDQuery>();
        }
    }
}
