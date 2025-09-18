using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Companies.Queries;

namespace KOG.ECommerce.Features.Companies.GetAllPendingCompany
{
    public record GetAllPendingCompanyRequestViewModel(int PageIndex = 1, int PageSize = 100);
    public class GetAllPendingCompanyRequestValidator : AbstractValidator<GetAllPendingCompanyRequestViewModel>
    {
        public GetAllPendingCompanyRequestValidator()
        {
        }
    }
    public class GetAllPendingCompanyRequestProfile : Profile
    {
        public GetAllPendingCompanyRequestProfile()
        {
            CreateMap<GetAllPendingCompanyRequestViewModel, GetAllPendingCompanyQuery>();
        }
    }
}
