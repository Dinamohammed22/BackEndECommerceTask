using AutoMapper;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using Microsoft.AspNetCore.Mvc; 

namespace KOG.ECommerce.Features.Orders.ExportOrdersToExcel
{
    public record ExportOrdersToExcelResponseViewModel(FileResult File);

    public class ExportOrdersToExcelResponseProfile : Profile
    {
        public ExportOrdersToExcelResponseProfile()
        {
            // Map ExportOrdersDTO to ExportOrdersToExcelResponseViewModel
            CreateMap<ExportOrdersDTO, ExportOrdersToExcelResponseViewModel>()
                .ForMember(dest => dest.File, opt => opt.MapFrom(src =>
                    new FileContentResult(src.FileContent, src.ContentType)
                    {
                        FileDownloadName = src.FileName
                    })
                );
        }
    }
    }
