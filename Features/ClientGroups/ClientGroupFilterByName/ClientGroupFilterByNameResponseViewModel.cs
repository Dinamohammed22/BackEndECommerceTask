using AutoMapper;
using KOG.ECommerce.Features.Common.ClientGroups.DTOs;

namespace KOG.ECommerce.Features.ClientGroups.ClientGroupFilterByName
{
    public record ClientGroupFilterByNameResponseViewModel(string ID, string Name, bool TaxExempted);
    public class ClientGroupFilterByNameResponseProfile : Profile
    {
        public ClientGroupFilterByNameResponseProfile()
        {
            CreateMap<ClientGroupProfileDTO, ClientGroupFilterByNameResponseViewModel>();
        }
    }
}
