using AutoMapper;
using KOG.ECommerce.Models.Advertisements;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Advertisements.DTOs
{
    public class GetAllAdvertisementDTO
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public ImageType ImageTypes { get; set; }
        public string? Hyperlink { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Path { get; set; } = "";
    }
        public class GetAllAdvertisementProfile:Profile
    {
        public GetAllAdvertisementProfile()
        {
            CreateMap<Advertisement, GetAllAdvertisementDTO>();
        }
    }
}
