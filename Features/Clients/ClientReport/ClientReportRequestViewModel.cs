using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Clients.Queries;

namespace KOG.ECommerce.Features.Clients.ClientReport
{
    public record ClientReportRequestViewModel(string? Name, DateTime? From,DateTime? To, int pageIndex = 1,int pageSize = 100);
    public class ClientReportRequestValidator : AbstractValidator<ClientReportRequestViewModel>
    {
        public ClientReportRequestValidator()
        {
        }
    }
    public class ClientReportRequestProfile : Profile
    {
        public ClientReportRequestProfile()
        {
            CreateMap<ClientReportRequestViewModel, ClientReportQuery>();
        }
    }
}
