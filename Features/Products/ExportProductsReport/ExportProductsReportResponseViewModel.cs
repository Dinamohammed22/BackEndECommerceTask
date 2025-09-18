using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;

namespace KOG.ECommerce.Features.Products.ExportProductsReport
{
    public record ExportProductsReportResponseViewModel(byte[] FileContent, string FileName, string ContentType);
    public class ExportProductsReportResponseProfile:Profile
    {
        public ExportProductsReportResponseProfile()
        {
            CreateMap<ExportProductsReportDTO, ExportProductsReportResponseViewModel>();
        }
    }
}
