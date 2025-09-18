using AutoMapper;
using KOG.ECommerce.Features.Common.Clients.DTOs;

namespace KOG.ECommerce.Features.Clients.ExportClientReportToExcel
{
    public record ExportClientReportToExcelResponseViewModel(byte[] FileContent, string FileName, string ContentType);
    public class ExportClientReportToExcelResponseProfile : Profile
    {
        public ExportClientReportToExcelResponseProfile()
        {
            CreateMap<ExportClientsDTO, ExportClientReportToExcelResponseViewModel>();
        }
    }
}
