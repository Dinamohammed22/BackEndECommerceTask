using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Advertisements.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Advertisements.GetAdvertisementByID
{
    public class GetAdvertisementByIDEndPoint : EndpointBase<GetAdvertisementByIDRequestViewModel, GetAdvertisementByIDResponseViewModel>
    {
        public GetAdvertisementByIDEndPoint(EndpointBaseParameters<GetAdvertisementByIDRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAdvertisementByID })]
        public async Task<EndPointResponse<GetAdvertisementByIDResponseViewModel>> GetAdvertisementByID([FromQuery] GetAdvertisementByIDRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<GetAdvertisementByIdQuery>());

            var response = result.Data.MapOne<GetAdvertisementByIDResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<GetAdvertisementByIDResponseViewModel>.Success(response, "Get Advertisement successfully.");
            else
                return EndPointResponse<GetAdvertisementByIDResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
