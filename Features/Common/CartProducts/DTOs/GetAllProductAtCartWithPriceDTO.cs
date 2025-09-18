using AutoMapper;
using KOG.ECommerce.Models.CartProducts;

namespace KOG.ECommerce.Features.Common.CartProducts.DTOs
{
    public class GetAllProductAtCartWithPriceDTO
    {
        public String ProductId { get; set; }
        public String ProductName { get; set; }
        public String CompanyId { get; set; }
        public String CompanyName { get; set; }
        public String Path { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
        public int NumberOfPoints { get; set; }
    }
    public class GetAllProductAtCartWithPriceDTOProfile : Profile
    {
        public GetAllProductAtCartWithPriceDTOProfile()
        {
            CreateMap<CartProduct, GetAllProductAtCartWithPriceDTO>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Product.CompanyId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Product.Company.Name))
                .ForMember(dest => dest.MinimumQuantity, opt => opt.MapFrom(src => src.Product.MinimumQuantity))
                .ForMember(dest => dest.MaximumQuantity, opt => opt.MapFrom(src => src.Product.MaximumQuantity));
        }
    }
}
