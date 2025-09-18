using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Brands.BulkActivateBrand.Orchisterator;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Brands.BulkActivateBrand
{
     public class BulkActivateBrandEndPoint : EndpointBase<BulkActivateBrandRequestViewModel, BulkActivateBrandResponseViewModel>
    {
        public BulkActivateBrandEndPoint(EndpointBaseParameters<BulkActivateBrandRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkActivateBrand })]
        public async Task<EndPointResponse<BulkActivateBrandResponseViewModel>> BulkActivateBrand(BulkActivateBrandRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkActivateBrandOrchisterator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkActivateBrandResponseViewModel>.Success(new BulkActivateBrandResponseViewModel(), "All Brands Activated Successfully");
            else
                return EndPointResponse<BulkActivateBrandResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
