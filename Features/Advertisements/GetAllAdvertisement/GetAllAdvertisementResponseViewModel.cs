using AutoMapper;
using KOG.ECommerce.Features.Common.Advertisements.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Advertisements.GetAllAdvertisement
{
    public record GetAllAdvertisementResponseViewModel(string ID,
        string Title,
        bool IsActive,
        ImageType ImageTypes,
        string? Hyperlink,
        DateTime StartDate,
        DateTime EndDate,
        string? Path = ""
    );
    public class GetAllAdvertisementResponseProfile : Profile
    {
        public GetAllAdvertisementResponseProfile()
        {
            CreateMap<GetAllAdvertisementDTO, GetAllAdvertisementResponseViewModel>();
        }
    }
}
