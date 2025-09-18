using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KOG.ECommerce.Features.Products.ExportProductsToExcel
{
    public record ExportProductsToExcelResponseViewModel(FileResult File);
    public class ExportProductsToExcelResponseProfile : Profile
    {
        public ExportProductsToExcelResponseProfile()
        {
            CreateMap<ExportProductsDTO, ExportProductsToExcelResponseViewModel>().ForMember(dest => dest.File, opt => opt.MapFrom(src =>
                    new FileContentResult(src.FileContent, src.ContentType)
                    {
                        FileDownloadName = src.FileName
                    }));
        }
    }
}
