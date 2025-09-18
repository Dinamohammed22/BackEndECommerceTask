using AutoMapper;
using KOG.ECommerce.Models.CartProducts;

namespace KOG.ECommerce.Features.Common.CartProducts.DTOs
{
    public class GetAllProductAtCartDTO
    {
        public String ProductId { get; set; }
        public String CompanyId { get; set; }
        public String CompanyName { get; set; }
        public int Quantity { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
    }

    public class GetAllProductAtCartDTOProfile : Profile
    {
        public GetAllProductAtCartDTOProfile()
        {
            CreateMap<CartProduct, GetAllProductAtCartDTO>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Product.CompanyId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Product.Company.Name))
                .ForMember(dest => dest.MinimumQuantity, opt => opt.MapFrom(src => src.Product.MinimumQuantity))
                .ForMember(dest => dest.MaximumQuantity, opt => opt.MapFrom(src => src.Product.MaximumQuantity));
        }
    }
}
