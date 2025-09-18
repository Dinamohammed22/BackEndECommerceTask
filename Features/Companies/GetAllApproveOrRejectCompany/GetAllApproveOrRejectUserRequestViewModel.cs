using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Companies.Queries;

namespace KOG.ECommerce.Features.Companies.GetAllApproveOrRejectCompany
{
    public record GetAllApproveOrRejectCompanyRequestViewModel(int PageIndex = 1, int PageSize = 100);
    public class GetAllApproveOrRejectCompanyRequestValidator : AbstractValidator<GetAllApproveOrRejectCompanyRequestViewModel>
    {
        public GetAllApproveOrRejectCompanyRequestValidator()
        {
        }
    }
    public class GetAllApproveOrRejectCompanyRequestProfile : Profile
    {
        public GetAllApproveOrRejectCompanyRequestProfile()
        {
            CreateMap<GetAllApproveOrRejectCompanyRequestViewModel, GetAllApproveOrRejectCompanyQuery>();
        }
    }
}
