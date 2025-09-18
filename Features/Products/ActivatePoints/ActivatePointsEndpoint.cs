using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Products.ActivatePoints.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Products.ActivatePoints
{
    public class ActivatePointsEndpoint : EndpointBase<ActivatePointsRequestViewModel, ActivatePointsResponseViewModel>
    {
        public ActivatePointsEndpoint(EndpointBaseParameters<ActivatePointsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ActivateProductPoint })]
        public async Task<EndPointResponse<ActivatePointsResponseViewModel>> ActivePoint(ActivatePointsRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ActivatePointsCommand>());
            if (result.IsSuccess)
                return EndPointResponse<ActivatePointsResponseViewModel>.Success(new ActivatePointsResponseViewModel(), "Product Point Activated successfully");
            else
                return EndPointResponse<ActivatePointsResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
