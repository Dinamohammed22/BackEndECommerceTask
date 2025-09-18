using AutoMapper;
using KOG.ECommerce.Models.ClientGroups;

namespace KOG.ECommerce.Features.Common.ClientGroups.DTOs
{
    public record ClientGroupProfileDTO(string ID, string Name, bool TaxExempted);
    public class ClientGroupFilterByNameProfile : Profile
    {
        public ClientGroupFilterByNameProfile()
        {
            CreateMap<ClientGroup, ClientGroupProfileDTO>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
