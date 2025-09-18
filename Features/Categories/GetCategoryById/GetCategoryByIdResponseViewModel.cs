using AutoMapper;
using KOG.ECommerce.Features.Common.Categories.DTOs;

namespace KOG.ECommerce.Features.Categories.GetCategoryById
{
    public record GetCategoryByIdResponseViewModel(string Id, string Name, string? ParentCategoryId, List<string>? Tags, List<string> SEO, string Description, bool IsActive);
    public class GetCategoryByIdResponseProfile : Profile
    {
        public GetCategoryByIdResponseProfile()
        {
            CreateMap<GetCategoryByIdDTO, GetCategoryByIdResponseViewModel>();
        }
    }
}
