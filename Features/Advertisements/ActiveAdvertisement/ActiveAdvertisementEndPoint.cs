using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Brands.ActiveBrand.Commands;
using KOG.ECommerce.Features.Brands.ActiveBrand;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Advertisements.ActiveAdvertisement.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Advertisements.ActiveAdvertisement
{
    public class ActiveAdvertisementEndPoint : EndpointBase<ActiveAdvertisementRequestViewModel, ActiveAdvertisementResponseViewModel>
    {
        public ActiveAdvertisementEndPoint(EndpointBaseParameters<ActiveAdvertisementRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ActiveAdvertisement })]
        public async Task<EndPointResponse<ActiveAdvertisementResponseViewModel>> ActiveAdvertisement(ActiveAdvertisementRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ActiveAdvertisementCommand>());
            if (result.IsSuccess)
                return EndPointResponse<ActiveAdvertisementResponseViewModel>.Success(new ActiveAdvertisementResponseViewModel(), "Advertisement Activated Successfully");
            else
                return EndPointResponse<ActiveAdvertisementResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
