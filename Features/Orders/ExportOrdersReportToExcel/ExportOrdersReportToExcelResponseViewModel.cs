using AutoMapper;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using Microsoft.AspNetCore.Mvc; 

namespace KOG.ECommerce.Features.Orders.ExportOrdersToExcel
{
    public record ExportOrdersReportToExcelResponseViewModel(FileResult File);

    public class ExportOrdersReportToExcelResponseProfile : Profile
    {
        public ExportOrdersReportToExcelResponseProfile()
        {
            // Map ExportOrdersDTO to ExportOrdersToExcelResponseViewModel
            CreateMap<ExportOrdersDTO, ExportOrdersReportToExcelResponseViewModel>()
                .ForMember(dest => dest.File, opt => opt.MapFrom(src =>
                    new FileContentResult(src.FileContent, src.ContentType)
                    {
                        FileDownloadName = src.FileName
                    })
                );
        }
    }
    }
