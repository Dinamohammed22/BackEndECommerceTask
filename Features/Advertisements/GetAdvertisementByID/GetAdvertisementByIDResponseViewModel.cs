using AutoMapper;
using KOG.ECommerce.Features.Common.Advertisements.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Advertisements.GetAdvertisementByID
{
    public record GetAdvertisementByIDResponseViewModel(
        string Title,
        bool IsActive,
        ImageType ImageTypes,
        string? Path,
        string? Hyperlink,
        DateTime StartDate,
        DateTime EndDate
    );
    public class GetAdvertisementByIDResponseProfile : Profile
    {
        public GetAdvertisementByIDResponseProfile()
        {
            CreateMap<GetAdvertisementByIdDTO, GetAdvertisementByIDResponseViewModel>();
        }
    }
}
