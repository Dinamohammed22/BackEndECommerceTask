using AutoMapper;
using KOG.ECommerce.Features.Common.Categories.DTOs;

namespace KOG.ECommerce.Features.Categories.GetCategoryChildren
{
    public class GetCategoryChildrenResponseViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool HasChildren { get; set; }

    }
    public class GetCategoryChildrenResponseProfile : Profile
    {
        public GetCategoryChildrenResponseProfile()
        {
            CreateMap<GetCategoryChildrenDTO, GetCategoryChildrenResponseViewModel>();
        }
    }
}
