using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Categories.Queries;

namespace KOG.ECommerce.Features.Categories.GetSubcategories
{
    public record GetSubcategoriesRequestViewModel(string CategoryId);
    public class GetSubcategoriesRequestValidator:AbstractValidator<GetSubcategoriesRequestViewModel>
    {
        public GetSubcategoriesRequestValidator()
        {

        }
    }

    public class GetSubcategoriesRequestProfile:Profile
    {
        public GetSubcategoriesRequestProfile()
        {
            CreateMap<GetSubcategoriesRequestViewModel, GetSubcategoriesQuery>();
        }
    }
}
