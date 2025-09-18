using AutoMapper;
using KOG.ECommerce.Features.Common.ClientGroups.DTOs;

namespace KOG.ECommerce.Features.ClientGroups.GetClientGroupByID
{
    public record GetClientGroupByIDResponseViewModel(string Name, bool TaxExempted);
    public class GetClientGroupByIDResponseProfile : Profile
    {
        public GetClientGroupByIDResponseProfile()
        {
            CreateMap<ClientGroupProfileDTO, GetClientGroupByIDResponseViewModel>();
        }
    }
}
