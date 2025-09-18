using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.ExportClientToExcel
{
    public record ExportClientToExcelRequestViewModel(string? Name, string? Email, string? Mobile, string? NationalNumber, string? ClientGroupId,
        VerifyStatus? VerifyStatus, DateTime? From, DateTime? To, Religion? Religion);
    public class ExportClientToExcelRequestValidator : AbstractValidator<ExportClientToExcelRequestViewModel>
    {
        public ExportClientToExcelRequestValidator()
        {
        }
    }
    public class ExportClientToExcelRequestProfile : Profile
    {
        public ExportClientToExcelRequestProfile() {
            CreateMap<ExportClientToExcelRequestViewModel, ExportClientToExcelQuery>();
        }
    }
}
