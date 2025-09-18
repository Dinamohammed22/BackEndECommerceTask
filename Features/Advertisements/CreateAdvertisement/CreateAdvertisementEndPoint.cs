using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Advertisements.CreateAdvertisement.Orchestrators;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Advertisements.CreateAdvertisement
{
    public class CreateAdvertisementEndPoint : EndpointBase<CreateAdvertisementRequestViewModel, CreateAdvertisementResponseViewModel>
    {
        public CreateAdvertisementEndPoint(EndpointBaseParameters<CreateAdvertisementRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreateAdvertisement })]
        public async Task<EndPointResponse<CreateAdvertisementResponseViewModel>> CreateAdvertisement (CreateAdvertisementRequestViewModel request)
        {
            var validationResult = await ValidateRequestAsync(request);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(request.MapOne<CreateAdvertisementOrchestrator>());
            if (result.IsSuccess)
            {
                return EndPointResponse<CreateAdvertisementResponseViewModel>.Success(new CreateAdvertisementResponseViewModel(), "Advertisement Added successfully.");
            }
            return EndPointResponse<CreateAdvertisementResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
