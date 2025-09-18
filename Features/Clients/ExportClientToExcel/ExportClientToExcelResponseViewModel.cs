using AutoMapper;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Features.Orders.ExportOrdersToExcel;
using Microsoft.AspNetCore.Mvc;

namespace KOG.ECommerce.Features.Clients.ExportClientToExcel
{
    public record ExportClientToExcelResponseViewModel(FileResult File);
    public class ExportClientToExcelResponseProfile : Profile
    {
        public ExportClientToExcelResponseProfile() {
            CreateMap<ExportClientsDTO, ExportOrdersToExcelResponseViewModel>()
                   .ForMember(dest => dest.File, opt => opt.MapFrom(src =>
                       new FileContentResult(src.FileContent, src.ContentType)
                       {
                           FileDownloadName = src.FileName
                       }));
        }
    }
}
