using AutoMapper;
using KOG.ECommerce.Models.CartProducts;

namespace KOG.ECommerce.Features.Common.CartProducts.DTOs
{
    public class GetAllProductAtCartWithCompanyIdDTO
    {
        public String ProductId { get; set; }
        public String CompanyId { get; set; }
        public int Quantity { get; set; }
    }
    public class GetAllProductAtCartWithCompanyIdDTOProfile : Profile
    {
        public GetAllProductAtCartWithCompanyIdDTOProfile()
        {
            CreateMap<CartProduct, GetAllProductAtCartWithCompanyIdDTO>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Product.CompanyId));
        }
    }
}
