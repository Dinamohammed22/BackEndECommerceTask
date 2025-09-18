using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Companies.Queries;

namespace KOG.ECommerce.Features.Companies.SelectListCompany
{
    public record SelectListCompanyRequestViewModel();
    public class SelectListCompanyRequestValidator : AbstractValidator<SelectListCompanyRequestViewModel>
    {
        public SelectListCompanyRequestValidator()
        {
        }
    }
    public class SelectListCompanyRequestProfile : Profile
    {
        public SelectListCompanyRequestProfile()
        {
            CreateMap<SelectListCompanyRequestViewModel, SelectListCompanyQuery>();
        }
    }
}
