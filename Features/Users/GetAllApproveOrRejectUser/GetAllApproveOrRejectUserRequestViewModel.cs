using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Users.Queries;

namespace KOG.ECommerce.Features.Users.GetAllApproveOrRejectUser
{
    public record GetAllApproveOrRejectUserRequestViewModel(int PageIndex = 1, int PageSize = 100);
    public class GetAllApproveOrRejectUserRequestValidator : AbstractValidator<GetAllApproveOrRejectUserRequestViewModel>
    {
        public GetAllApproveOrRejectUserRequestValidator()
        {
        }
    }
    public class GetAllApproveOrRejectUserRequestProfile : Profile
    {
        public GetAllApproveOrRejectUserRequestProfile()
        {
            CreateMap<GetAllApproveOrRejectUserRequestViewModel, GetAllApproveOrRejectUserQuery>();
        }
    }
}
