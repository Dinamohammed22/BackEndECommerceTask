using AutoMapper;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using Microsoft.AspNetCore.Mvc; 

namespace KOG.ECommerce.Features.Companies.ExportCompanyToExcel
{
    public record ExportCompanyToExcelResponseViewModel(FileResult File);

    public class ExportCompanyToExcelResponseProfile : Profile
    {
        public ExportCompanyToExcelResponseProfile()
        {
            CreateMap<ExportCompanyDTO, ExportCompanyToExcelResponseViewModel>()
                .ForMember(dest => dest.File, opt => opt.MapFrom(src =>
                    new FileContentResult(src.FileContent, src.ContentType)
                    {
                        FileDownloadName = src.FileName
                    })
                );
        }
    }
    }
