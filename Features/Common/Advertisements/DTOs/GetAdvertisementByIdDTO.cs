using AutoMapper;
using KOG.ECommerce.Models.Advertisements;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Advertisements.DTOs
{
    public record GetAdvertisementByIdDTO(
        string Title,
        bool IsActive,
        ImageType ImageTypes,
        string? Hyperlink,
        DateTime StartDate,
        DateTime EndDate,
        string? Path=""
    );
    public class GetAdvertisementByIdDTOProfile : Profile
    {
        public GetAdvertisementByIdDTOProfile()
        {
            CreateMap<Advertisement, GetAdvertisementByIdDTO>();
        }
    }
}
