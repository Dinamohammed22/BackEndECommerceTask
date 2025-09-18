using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.RoleFeatures.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.RoleFeatures.GetAllFeaturesListed
{
    public class GetAllFeaturesListedEndPoint : EndpointBase<GetAllFeaturesListedRequestViewModel, List<GetAllFeaturesListedResponseViewModel>>
    {
        public GetAllFeaturesListedEndPoint(EndpointBaseParameters<GetAllFeaturesListedRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetFeatueresListed })]
        public async Task<EndPointResponse<List<GetAllFeaturesListedResponseViewModel>>> GetFeatueresListed([FromQuery]GetAllFeaturesListedRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<GetAllFeaturesListedQuery>());

            List<GetAllFeaturesListedResponseViewModel> response = result.Data.MapList<GetAllFeaturesListedResponseViewModel>().ToList();

            return EndPointResponse<List<GetAllFeaturesListedResponseViewModel>>
                .Success(response, "Features retrieved successfully.");
        }
    }
}
