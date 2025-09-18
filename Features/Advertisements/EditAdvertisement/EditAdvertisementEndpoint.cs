using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Advertisements.EditAdvertisement.Orchestrators;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Advertisements.EditAdvertisement
{
    public class EditAdvertisementEndpoint : EndpointBase<EditAdvertisementRequestViewModel, EditAdvertisementResponseViewModel>
    {
        public EditAdvertisementEndpoint(EndpointBaseParameters<EditAdvertisementRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditAdvertisement })]
        public async Task<EndPointResponse<EditAdvertisementResponseViewModel>> EditAdvertisement(EditAdvertisementRequestViewModel request)
        {
            var validationResult = await ValidateRequestAsync(request);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(request.MapOne<EditAdvertisementOrchestrtor>());
            if (result.IsSuccess)
            {
                return EndPointResponse<EditAdvertisementResponseViewModel>.Success(new EditAdvertisementResponseViewModel(), "Advertisement Edited successfully.");
            }
            return EndPointResponse<EditAdvertisementResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
