using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Categories.GetCategory;
using KOG.ECommerce.Features.Common.Categories.Queries;

namespace KOG.ECommerce.Features.Common.Categories.DTOs
{
    public record GetCategoryRequestViewModel();

    public class GetCategoryRequestValidator : AbstractValidator<GetCategoryRequestViewModel>
    {
        public GetCategoryRequestValidator()
        {
            
        }
    }

    public class CategoryFilterResponseProfile : Profile
    {
        public CategoryFilterResponseProfile()
        {
            CreateMap<GetCategoryRequestViewModel, GetCategoryQuery>();
        }
    }
}
