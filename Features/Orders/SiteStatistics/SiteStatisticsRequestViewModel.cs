using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Features.Orders.SiteStatistics.Orchestrators;

namespace KOG.ECommerce.Features.Orders.SiteStatistics
{
    public record SiteStatisticsRequestViewModel(DateTime? From,DateTime? To);
    public class SiteStatisticsRequestValidator : AbstractValidator<SiteStatisticsRequestViewModel>
    {
        public SiteStatisticsRequestValidator() { }
    }
    public class SiteStatisticsRequestProfile : Profile
    {
        public SiteStatisticsRequestProfile()
        {
            CreateMap<SiteStatisticsRequestViewModel, SiteStatisticsOrchestrator>();
            CreateMap<SiteStatisticsOrchestrator, CompanyStatisticsQuery>();
            CreateMap<SiteStatisticsOrchestrator, SiteStatisticsMainQuery>();
            CreateMap<SiteStatisticsMainQuery, SalesStatisticsQuery>();
            CreateMap<SiteStatisticsMainQuery, ClientsStatisticsQuery>();
            CreateMap<CompanyStatisticsQuery, CompanySalesStatisticsQuery>();
            CreateMap<CompanyStatisticsQuery, ClientsStatisticsQuery>();
        }
    }
}
