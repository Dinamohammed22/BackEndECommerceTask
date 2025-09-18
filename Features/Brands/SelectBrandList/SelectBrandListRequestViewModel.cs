using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Brands.Queries;

namespace KOG.ECommerce.Features.Brands.SelectBrandList
{
    public class SelectBrandListRequestViewModel();
    public class SelectBrandListRequestValidator : AbstractValidator<SelectBrandListRequestViewModel>
    {
        public SelectBrandListRequestValidator()
        {
        }
    }
    public class SelectBrandListRequestProfile : Profile
    {
        public SelectBrandListRequestProfile() {
            CreateMap<SelectBrandListRequestViewModel, SelectBrandListQuery>();
        } 
    }
}
