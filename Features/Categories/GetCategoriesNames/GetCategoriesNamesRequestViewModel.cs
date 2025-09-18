using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Categories.Queries;

namespace KOG.ECommerce.Features.Categories.GetCategoriesNames
{
    public record GetCategoriesNamesRequestViewModel();
    public class GetCategoriesNamesRequestValidator:AbstractValidator<GetCategoriesNamesRequestViewModel>
    {
        public GetCategoriesNamesRequestValidator() { }
    }
    public class GetCategoriesNamesRequestProfile : Profile
    {
        public GetCategoriesNamesRequestProfile()
        {
            CreateMap<GetCategoriesNamesRequestViewModel, GetCategoriesNamesQuery>();
        }

    }
}
