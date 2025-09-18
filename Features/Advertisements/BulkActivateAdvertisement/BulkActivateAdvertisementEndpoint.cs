using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Advertisements.BulkActivateAdvertisement.Orchisterators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Advertisements.BulkActivateAdvertisement
{
    public class BulkActivateAdvertisementEndpoint : EndpointBase<BulkActivateAdvertisementRequestViewModel, BulkActivateAdvertisementResponseViewModel>
    {
        public BulkActivateAdvertisementEndpoint(EndpointBaseParameters<BulkActivateAdvertisementRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkActivateAdvertisement })]
        public async Task<EndPointResponse<BulkActivateAdvertisementResponseViewModel>> BulkActivateAdvertisement(BulkActivateAdvertisementRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkActivateAdvertisementOrchisterator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkActivateAdvertisementResponseViewModel>.Success(new BulkActivateAdvertisementResponseViewModel(), "All Advertisements Activated Successfully");
            else
                return EndPointResponse<BulkActivateAdvertisementResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
