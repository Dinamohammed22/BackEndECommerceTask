using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Categories.Queries;

namespace KOG.ECommerce.Features.Categories.GetCategoryChildren
{
    public record GetCategoryChildrenRequestViewModel(string? ParentCategoryId);
    public class GetCategoryChildrenRequestValidator : AbstractValidator<GetCategoryChildrenRequestViewModel>
    {
        public GetCategoryChildrenRequestValidator()
        {
        }
    }
    public class GetCategoryChildrenRequestProfile : Profile
    {
        public GetCategoryChildrenRequestProfile()
        {
            CreateMap<GetCategoryChildrenRequestViewModel, GetCategoryChildrenQuery>();
        }
    }
}
