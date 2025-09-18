using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Categories.Queries;

namespace KOG.ECommerce.Features.Categories.GetCategoryIndex
{
    public record GetCategoryIndexRequestViewModel(string? CategoryId, string? SubCategoryId, int pageIndex = 1, int pageSize = 100);
    public class GetCategoryIndexRequestValidator : AbstractValidator<GetCategoryIndexRequestViewModel>
    {
        public GetCategoryIndexRequestValidator()
        {
        }
    }
    public class GetCategoryIndexRequestProfile : Profile
    {
        public GetCategoryIndexRequestProfile() {
            CreateMap<GetCategoryIndexRequestViewModel, GetAllCategoryIndexQuery>();
        }
    }
}
