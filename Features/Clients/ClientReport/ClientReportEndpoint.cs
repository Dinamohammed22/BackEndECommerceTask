using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Clients.ClientReport
{
    public class ClientReportEndpoint : EndpointBase<ClientReportRequestViewModel, ClientReportResponseViewModel>
    {
        public ClientReportEndpoint(EndpointBaseParameters<ClientReportRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ClientReport })]
        public async Task<EndPointResponse<PagingViewModel<ClientReportResponseViewModel>>> ClientReport([FromQuery] ClientReportRequestViewModel? viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<ClientReportQuery>());

            var response = result.Data.MapPage<ClientReportDTO, ClientReportResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<PagingViewModel<ClientReportResponseViewModel>>.Success(response, "Client report retrieved successfully.");
            else
                return EndPointResponse<PagingViewModel<ClientReportResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
