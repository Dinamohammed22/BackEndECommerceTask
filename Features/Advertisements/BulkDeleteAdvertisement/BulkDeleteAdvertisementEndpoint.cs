using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Advertisements.BulkDeleteAdvertisement.Orchisterators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Advertisements.BulkDeleteAdvertisement
{
    public class BulkDeleteAdvertisementEndpoint : EndpointBase<BulkDeleteAdvertisementRequestViewModel, BulkDeleteAdvertisementResponseViewModel>
    {
        public BulkDeleteAdvertisementEndpoint(EndpointBaseParameters<BulkDeleteAdvertisementRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeleteAdvertisement })]
        public async Task<EndPointResponse<BulkDeleteAdvertisementResponseViewModel>> BulkDeletedAdvertisement(BulkDeleteAdvertisementRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeleteAdvertisementOrchisterator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeleteAdvertisementResponseViewModel>.Success(new BulkDeleteAdvertisementResponseViewModel(), "All Advertisements Deleted Successfully");
            else
                return EndPointResponse<BulkDeleteAdvertisementResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
