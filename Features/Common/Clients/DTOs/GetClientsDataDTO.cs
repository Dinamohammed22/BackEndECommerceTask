using AutoMapper;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.Common.Clients.DTOs
{
    public record GetClientsDataDTO(
        string Name,
        string Email
    );
    public class GetClientsDataDTOProfile : Profile
    {
        public GetClientsDataDTOProfile()
        {
            CreateMap<Client, GetClientsDataDTO>();
        }
    }
}
