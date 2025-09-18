using AutoMapper;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.Common.Clients.DTOs
{
    public record ClientsStatisticsDTO(double NumberOfCustomers, double CustomersWaitingApproval);
    public class ClientsStatisticsProfile : Profile
    {
        public ClientsStatisticsProfile() { 
            CreateMap<Client, ClientsStatisticsDTO>();
        }
    }
}
