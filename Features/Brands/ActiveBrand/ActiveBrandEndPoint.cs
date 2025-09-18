using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Features.Brands.ActiveBrand.Commands;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Brands.ActiveBrand
{
    public class ActiveBrandEndPoint : EndpointBase<ActiveBrandRequestViewModel, ActiveBrandResponseViewModel>
    {
        public ActiveBrandEndPoint(EndpointBaseParameters<ActiveBrandRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ActiveBrand })]
        public async Task<EndPointResponse<ActiveBrandResponseViewModel>> ActiveBrand(ActiveBrandRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ActiveBrandCommand>());
            if (result.IsSuccess)
                return EndPointResponse<ActiveBrandResponseViewModel>.Success(new ActiveBrandResponseViewModel(), "Brand Activated Successfully");
            else
                return EndPointResponse<ActiveBrandResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
