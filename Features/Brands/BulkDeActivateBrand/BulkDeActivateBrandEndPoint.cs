using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Brands.BulkActivateBrand.Orchisterator;
using KOG.ECommerce.Features.Brands.BulkActivateBrand;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Brands.BulkDeActivateBrand.Orchisterator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Brands.BulkDeActivateBrand
{
     public class BulkDeActivateBrandEndPoint : EndpointBase<BulkDeActivateBrandRequestViewModel, BulkDeActivateBrandResponseViewModel>
    {
        public BulkDeActivateBrandEndPoint(EndpointBaseParameters<BulkDeActivateBrandRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeActivateBrand })]
        public async Task<EndPointResponse<BulkDeActivateBrandResponseViewModel>> BulkDeActivateBrand(BulkDeActivateBrandRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeActivateBrandOrchisterator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeActivateBrandResponseViewModel>.Success(new BulkDeActivateBrandResponseViewModel(), "All Brands DeActivated Successfully");
            else
                return EndPointResponse<BulkDeActivateBrandResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
