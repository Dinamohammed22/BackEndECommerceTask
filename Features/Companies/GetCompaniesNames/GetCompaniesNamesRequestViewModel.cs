using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Companies.Queries;

namespace KOG.ECommerce.Features.Companies.GetCompaniesNames
{
    public record GetCompaniesNamesRequestViewModel(List<string>? ClassificationId);
    public class GetCompaniesNamesRequestValidator:AbstractValidator<GetCompaniesNamesRequestViewModel>
    {
        public GetCompaniesNamesRequestValidator() { }
    }
    public class GetCompaniesNamesRequestProfile:Profile
    {
        public GetCompaniesNamesRequestProfile()
        {
            CreateMap<GetCompaniesNamesRequestViewModel, GetCompaniesNamesQuery>();
        }
    }
}
