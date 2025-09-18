using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Features.Common.Categories.Queries;

namespace KOG.ECommerce.Features.Categories.FilterCategory
{
    public record FilterCategoryRequestViewModel(string? Name);
    public class CategoryFilterRequestValidator : AbstractValidator<FilterCategoryRequestViewModel>
    {
        public CategoryFilterRequestValidator()
        {

        }
    }

    public class CategoryFilterResponseProfile : Profile
    {
        public CategoryFilterResponseProfile()
        {
            CreateMap<FilterCategoryRequestViewModel, FilterCategoryQuery>();
        }
    }
}
