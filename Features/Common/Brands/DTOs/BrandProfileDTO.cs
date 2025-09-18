using AutoMapper;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Features.Common.Medias.DTOs;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Common.Brands.DTOs
{
    public record BrandProfileDTO {
        public string ID{ get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<string>? Tags { get; set; }
        public string? Path { get; set; }
        public List<MediaDTO>? Media { get; set; } = null;
    };

    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandProfileDTO>()
                  .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags ?? new List<string>()));
        }
    }

}
