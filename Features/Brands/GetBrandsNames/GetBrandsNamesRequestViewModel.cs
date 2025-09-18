using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Brands.Queries;

namespace KOG.ECommerce.Features.Brands.GetBrandsNames
{
    public record GetBrandsNamesRequestViewModel(List<string>? CategoryIds):IRequestBase<GetBrandsNamesResponseViewModel>;
    public class GetBrandsNamesRequestValidator : AbstractValidator<GetBrandsNamesRequestViewModel>
    {
        public GetBrandsNamesRequestValidator()
        {
        }
    }
    public class GetBrandsNamesRequestProfile : Profile
    {
        public GetBrandsNamesRequestProfile()
        {
            CreateMap<GetBrandsNamesRequestViewModel, GetBrandsNamesQuery>();
        }
    }
}
