using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Brands.Queries;

namespace KOG.ECommerce.Features.Brands.GetAllBrand
{
    public record GetAllBrandRequestViewModel(int? NumberOfBrand);
    public class GetAllBrandRequestValidator : AbstractValidator<GetAllBrandRequestViewModel>
    {
        public GetAllBrandRequestValidator() { }
    }
    public class GetAllBrandRequestProfile : Profile
    {
        public GetAllBrandRequestProfile() {
            CreateMap<GetAllBrandRequestViewModel, GetAllBrandQuery>();
        }
    }
}
