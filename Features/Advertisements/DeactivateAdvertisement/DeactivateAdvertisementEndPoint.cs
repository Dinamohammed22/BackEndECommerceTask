using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Advertisements.ActiveAdvertisement.Commands;
using KOG.ECommerce.Features.Advertisements.ActiveAdvertisement;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Advertisements.DeactivateAdvertisement.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Advertisements.DeactivateAdvertisement
{
    public class DeactivateAdvertisementEndPoint : EndpointBase<DeactivateAdvertisementRequestViewModel, DeactivateAdvertisementResponseViewModel>
    {
        public DeactivateAdvertisementEndPoint(EndpointBaseParameters<DeactivateAdvertisementRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeactivateAdvertisement })]
        public async Task<EndPointResponse<DeactivateAdvertisementResponseViewModel>> DeactivateAdvertisement(DeactivateAdvertisementRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeactivateAdvertisementCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeactivateAdvertisementResponseViewModel>.Success(new DeactivateAdvertisementResponseViewModel(), "Advertisement DeActivated Successfully");
            else
                return EndPointResponse<DeactivateAdvertisementResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
