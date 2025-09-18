using AutoMapper;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using Microsoft.AspNetCore.Mvc; 

namespace KOG.ECommerce.Features.Companies.ExportCompanyReportsToExcel
{
    public record ExportCompanyReportsToExcelResponseViewModel(FileResult File);

    public class ExportCompanyReportsToExcelResponseProfile : Profile
    {
        public ExportCompanyReportsToExcelResponseProfile()
        {
            CreateMap<ExportCompanyDTO, ExportCompanyReportsToExcelResponseViewModel>()
                .ForMember(dest => dest.File, opt => opt.MapFrom(src =>
                    new FileContentResult(src.FileContent, src.ContentType)
                    {
                        FileDownloadName = src.FileName
                    })
                );
        }
    }
    }
