using AutoMapper;
using KOG.ECommerce.Features.Common.Classifications.DTOs;
using KOG.ECommerce.Features.Common.Companies.DTOs;

namespace KOG.ECommerce.Features.Classifications.GetAllClassifications
{
    public record GetAllClassificationsResponseViewModel(
         string? ID ,
         string? Name ,
         List<CompanyForClassificationDTO>? Companies 
        );
    public class GetAllClassificationsResponseProfile:Profile
    {
        public GetAllClassificationsResponseProfile()
        {
            CreateMap<GetAllClassificationsDTO, GetAllClassificationsResponseViewModel>();
        }
    }


}
