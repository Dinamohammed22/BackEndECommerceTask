using AutoMapper;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;

namespace KOG.ECommerce.Features.CartProducts.GetAllProductAtCartWithPrice
{
    public class GetAllProductAtCartWithPriceResponseViewModel
    {
        public List<GetAllProductAtCartWithPriceDTO> GetAllProducts { get; set; }
        public double TotalPrice { get; set; }

    }
    public class GetAllProductAtCartWithPriceResponseProfile : Profile
    {
        public GetAllProductAtCartWithPriceResponseProfile()
        {
            CreateMap<GetAllProductAtCartWithTotalPriceDTO, GetAllProductAtCartWithPriceResponseViewModel>()
                .ForMember(dest => dest.GetAllProducts, opt => opt.MapFrom(src => src.GetAllProducts))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));
        }
    }
}
