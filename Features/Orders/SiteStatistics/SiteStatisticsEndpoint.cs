using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Orders.SiteStatistics.Orchestrators;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Orders.SiteStatistics
{
    public class SiteStatisticsEndpoint : EndpointBase<SiteStatisticsRequestViewModel, SiteStatisticsResponseViewModel>
    {
        public SiteStatisticsEndpoint(EndpointBaseParameters<SiteStatisticsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.SiteStatistics })]
        public async Task<EndPointResponse<SiteStatisticsResponseViewModel>> Get([FromQuery]SiteStatisticsRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<SiteStatisticsOrchestrator>());
            var response = result.Data.MapOne<SiteStatisticsResponseViewModel>();
            if (result.IsSuccess)
                return EndPointResponse<SiteStatisticsResponseViewModel>.Success(response, "Get Site Statistics Successfully");
            else
                return EndPointResponse<SiteStatisticsResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
