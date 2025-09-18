using AutoMapper;
using KOG.ECommerce.Features.Common.Classifications.DTOs;

namespace KOG.ECommerce.Features.Classifications.GetClassificationsNames
{
    public record GetClassificationsNamesResponseViewModel(string ID, string Name, int NumberOfProducts);
    public class GetClassificationsNamesResponseProfile : Profile
    {
        public GetClassificationsNamesResponseProfile()
        {
            CreateMap<GetClassificationsNamesDTO, GetClassificationsNamesResponseViewModel>();
        }
    }
}
