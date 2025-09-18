using AutoMapper;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.ClientReport
{
    public record ClientReportResponseViewModel(string ID,string Name, ClientActivity? ClientActivity, string Mobile, double TotalPrice,
         double TotalLiter);
    public class ClientReportResponseProfile : Profile
    {
        public ClientReportResponseProfile()
        {
            CreateMap<ClientReportDTO, ClientReportResponseViewModel>();
        }
    }
}
