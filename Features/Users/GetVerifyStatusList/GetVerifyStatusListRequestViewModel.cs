using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Users.GetVerifyStatusList.Queries;

namespace KOG.ECommerce.Features.Users.GetVerifyStatusList
{
    public record GetVerifyStatusListRequestViewModel();
    public class GetVerifyStatusListRequestValidator : AbstractValidator<GetVerifyStatusListRequestViewModel>
    {
        public GetVerifyStatusListRequestValidator()
        {
        }
    }
    public class GetVerifyStatusListRequestProfile : Profile
    {
        public GetVerifyStatusListRequestProfile() {
            CreateMap<GetVerifyStatusListRequestViewModel, GetVerifyStatusListQuery>();
        }
    }
}
