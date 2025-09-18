using AutoMapper;
using KOG.ECommerce.Models.Clients;


namespace KOG.ECommerce.Features.Common.Clients.DTOs
{
    public record GetClientByGroupIDProfileDTO(string Name);
    public class GetClientsByGroupIDProfile : Profile
    {
        public GetClientsByGroupIDProfile()
        {
            CreateMap<Client, GetClientByGroupIDProfileDTO>();
        }
    }


}
