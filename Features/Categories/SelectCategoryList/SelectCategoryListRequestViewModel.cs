using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Categories.Queries;

namespace KOG.ECommerce.Features.Categories.SelectCategoryList
{
    public record SelectCategoryListRequestViewModel();
    public class SelectCategoryListRequestValidator : AbstractValidator<SelectCategoryListRequestViewModel>
    {
        public SelectCategoryListRequestValidator()
        {
        }
    }
    public class SelectCategoryListRequestProfile : Profile
    {
        public SelectCategoryListRequestProfile() {
            CreateMap<SelectCategoryListRequestViewModel, SelectCategoryListQuery>();
        }
    }
}
