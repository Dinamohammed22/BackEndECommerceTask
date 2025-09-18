using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;

namespace KOG.ECommerce.Features.Products.GetProductsByCommonTags
{
    public record GetProductsByCommonTagsResponseViewModel(
        string ID,
        string ProductName,
        double Price,
        string? Path, string CompanyId, string CompanyName
    );

    public class GetProductsByCommonTagsProfile : Profile
    {
        public GetProductsByCommonTagsProfile()
        {
            CreateMap<ProductViewDTO, GetProductsByCommonTagsResponseViewModel>();
        }
    }
}
