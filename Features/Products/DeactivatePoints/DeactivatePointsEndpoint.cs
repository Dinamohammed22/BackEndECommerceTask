using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Products.DeactivatePoints.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Products.DeactivatePoints
{
    public class DeactivatePointsEndpoint : EndpointBase<DeactivatePointsRequestViewModel, DeactivatePointsResponseViewModel>
    {
        public DeactivatePointsEndpoint(EndpointBaseParameters<DeactivatePointsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeactivateProductPoint })]
        public async Task<EndPointResponse<DeactivatePointsResponseViewModel>> DeactivatePoint(DeactivatePointsRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeactivatePointsCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeactivatePointsResponseViewModel>.Success(new DeactivatePointsResponseViewModel(), "Product Point Deactivated successfully");
            else
                return EndPointResponse<DeactivatePointsResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
