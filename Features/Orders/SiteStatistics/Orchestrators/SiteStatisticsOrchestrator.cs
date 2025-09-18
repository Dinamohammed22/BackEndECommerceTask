using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Orders.SiteStatistics.Orchestrators
{
    public record SiteStatisticsOrchestrator(DateTime? From, DateTime? To) : IRequestBase<SiteStatisticsDTO>;

    public class SiteStatisticsOrchestratorsHandler : RequestHandlerBase<Product, SiteStatisticsOrchestrator, SiteStatisticsDTO>
    {
        public SiteStatisticsOrchestratorsHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<SiteStatisticsDTO>> Handle(SiteStatisticsOrchestrator request, CancellationToken cancellationToken)
        {
            var role = _userState.RoleID;

            RequestResult<SiteStatisticsDTO> result = null;

            if (role == Role.Company)
            {
                result = await _mediator.Send(request.MapOne<CompanyStatisticsQuery>());
            }
            else
            {
                result = await _mediator.Send(request.MapOne<SiteStatisticsMainQuery>());
            }

            return RequestResult<SiteStatisticsDTO>.Success(result.Data);
        }
    }
}
