using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Companies.Queries;

namespace KOG.ECommerce.Features.Companies.GetCompanyRequestByID
{
    public record GetCompanyRequestByIDRequestViewModel(string ID);
    public class GetCompanyRequestByIDRequestValidator : AbstractValidator<GetCompanyRequestByIDRequestViewModel>
    {
        public GetCompanyRequestByIDRequestValidator()
        {
        }
    }
    public class GetCompanyRequestByIDRequestProfile : Profile
    {
        public GetCompanyRequestByIDRequestProfile()
        {
            CreateMap<GetCompanyRequestByIDRequestViewModel, GetCompanyRequestByIDQuery>();
        }
    }
}
