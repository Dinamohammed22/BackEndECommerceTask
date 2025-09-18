using AutoMapper;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Clients.DTOs
{
    public class ClientReportDTO
    {
        public string ID {  get; set; }
        public string Name { get; set; }
        public ClientActivity? ClientActivity { get; set; }
        public string Mobile { get; set; }
        public double TotalPrice { get; set; }
        public double TotalLiter { get; set; }
    }
    public class ClientReportDTOProfile : Profile
    {
        public ClientReportDTOProfile()
        {
            CreateMap<Client, ClientReportDTO>();
        }
    }
}
