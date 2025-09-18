using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Features.Brands.DeactiveBrand.Commands;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Brands.DeactiveBrand
{
    public class DeactiveBrandEndPoint : EndpointBase<DeactiveBrandRequestViewModel, DeactiveBrandResponseViewModel>
    {
        public DeactiveBrandEndPoint(EndpointBaseParameters<DeactiveBrandRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeactiveBrand })]
        public async Task<EndPointResponse<DeactiveBrandResponseViewModel>> DeactiveBrand(DeactiveBrandRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeactiveBrandCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeactiveBrandResponseViewModel>.Success(new DeactiveBrandResponseViewModel(), "Brand Deactivated Successfully");
            else
                return EndPointResponse<DeactiveBrandResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
