using AutoMapper;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
    public class GetProductNameAndPriceDTO
    {
        public string ID { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
    }
    public class GetProductNameAndPriceProfileDTO:Profile
    {
        public GetProductNameAndPriceProfileDTO()
        {
            CreateMap<Product, GetProductNameAndPriceDTO>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));
        }
    }

}
