using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Advertisements.BulkDeactivateAdvertisement.Orchisterators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Advertisements.BulkDeactivateAdvertisement
{
    public class BulkDeactivateAdvertisementEndpoint : EndpointBase<BulkDeactivateAdvertisementRequestViewModel, BulkDeactivateAdvertisementResponseViewModel>
    {
        public BulkDeactivateAdvertisementEndpoint(EndpointBaseParameters<BulkDeactivateAdvertisementRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeactivateAdvertisement })]
        public async Task<EndPointResponse<BulkDeactivateAdvertisementResponseViewModel>> BulkDeactivateAdvertisement(BulkDeactivateAdvertisementRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeactivateAdvertisementOrchisterator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeactivateAdvertisementResponseViewModel>.Success(new BulkDeactivateAdvertisementResponseViewModel(), "All Advertisements Deactivated Successfully");
            else
                return EndPointResponse<BulkDeactivateAdvertisementResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
