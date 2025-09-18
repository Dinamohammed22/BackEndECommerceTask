using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Categories.Queries;

namespace KOG.ECommerce.Features.Categories.SelectSubcategoryList
{
    public record SelectSubcategoryListRequestViewModel(string CategoryId);
    public class SelectSubcategoryListRequestValidator:AbstractValidator<SelectSubcategoryListRequestViewModel>
    {
        public SelectSubcategoryListRequestValidator() { }
    }
    public class SelectSubcategoryListRequestProfile:Profile
    {
        public SelectSubcategoryListRequestProfile()
        {
            CreateMap<SelectSubcategoryListRequestViewModel, SelectSubcategoryListQuery>();

        }
    }
}
