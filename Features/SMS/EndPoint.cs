using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Advertisements.CreateAdvertisement.Orchestrators;
using KOG.ECommerce.Features.Advertisements.CreateAdvertisement;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.SMS
{
    public class EndPoint : EndpointBase<request, Response>
    {
        public EndPoint(EndpointBaseParameters<request> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        public async Task<EndPointResponse<Response>> post(request request)
        {
            var validationResult = await ValidateRequestAsync(request);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(request.MapOne<Command>());
            if (result.IsSuccess)
            {
                return EndPointResponse<Response>.Success(new Response(), "sms sent successfully.");
            }
            return EndPointResponse<Response>.Failure(result.ErrorCode);
        }
    }
}
