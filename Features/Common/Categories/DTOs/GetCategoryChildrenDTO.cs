using AutoMapper;
using KOG.ECommerce.Models.Categories;

namespace KOG.ECommerce.Features.Common.Categories.DTOs
{
    public class GetCategoryChildrenDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool HasChildren { get; set; }

    }
    public class GetCategoryChildrenDTOProfile : Profile
    {
        public GetCategoryChildrenDTOProfile()
        {
            CreateMap<Category, GetCategoryChildrenDTO>()
                .ForMember(dest => dest.HasChildren,
                    opt => opt.MapFrom(src =>
                        src.Subcategories != null && src.Subcategories.Any(sc => sc.IsActive)));
        }
    }
}
