using AutoMapper;
using KOG.ECommerce.Models.Brands;

namespace KOG.ECommerce.Features.Common.Brands.DTOs
{
    public record GetAllBrandDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string? Path { get; set; }
        public List<string> Tags { get; set; }
    };
    public class GetAllBrandDTOProfile : Profile
    {
        public GetAllBrandDTOProfile()
        {
            CreateMap<Brand, GetAllBrandDTO>();
        }
    }
}
