using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Categories.Queries;

namespace KOG.ECommerce.Features.Categories.GetCategoryById
{
    public record GetCategoryByIdRequestViewModel(string ID);
    public class GetCategoryByIdRequestValidator : AbstractValidator<GetCategoryByIdRequestViewModel>
    {
        public GetCategoryByIdRequestValidator()
        {
        }
    }
    public class GetCategoryByIdRequestProfile : Profile
    {
        public GetCategoryByIdRequestProfile() {
            CreateMap<GetCategoryByIdRequestViewModel, GetCategoryByIdQuery>();
        }
    }
}
