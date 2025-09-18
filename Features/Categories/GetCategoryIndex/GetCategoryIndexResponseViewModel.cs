using AutoMapper;
using KOG.ECommerce.Features.Common.Categories.DTOs;

namespace KOG.ECommerce.Features.Categories.GetCategoryIndex
{
    public record GetCategoryIndexResponseViewModel(string ID,
        string Name,
        string? ParentCategoryId,
        int ProductCount,
        int SubcategoryCount,
        bool IsActive,
        string ImagePath);
    public class GetCategoryIndexResponseProfile : Profile
    {
        public GetCategoryIndexResponseProfile()
        {
            CreateMap<GetAllCategoryIndexDTO, GetCategoryIndexResponseViewModel>();
        }
    }
}
