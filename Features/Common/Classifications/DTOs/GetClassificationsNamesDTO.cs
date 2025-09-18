using AutoMapper;
using KOG.ECommerce.Models.Classifications;

namespace KOG.ECommerce.Features.Common.Classifications.DTOs
{
    public record GetClassificationsNamesDTO(string ID, string Name, int NumberOfProducts);
    public class GetClassificationsNamesDTOProfile : Profile
    {
        public GetClassificationsNamesDTOProfile()
        {
            CreateMap<Classification, GetClassificationsNamesDTO>();
        }
    }
}
