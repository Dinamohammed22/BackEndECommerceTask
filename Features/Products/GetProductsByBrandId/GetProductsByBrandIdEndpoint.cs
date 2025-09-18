using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.GetProductsByBrandId
{
    public class GetProductsByBrandIdEndpoint : EndpointBase<GetProductsByBrandIdRequestViewModel, GetProductsByBrandIdResponseViewModel>
    {
        public GetProductsByBrandIdEndpoint(EndpointBaseParameters<GetProductsByBrandIdRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetProductsByBrandId })]
        public async Task<EndPointResponse<IEnumerable<GetProductsByBrandIdResponseViewModel>>> GetProductsByBrandId([FromQuery] GetProductsByBrandIdRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetProductsByBrandIdQuery>());

            var response = result.Data.MapList<GetProductsByBrandIdResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<IEnumerable<GetProductsByBrandIdResponseViewModel>>
                    .Success(response, "Get Products successfully.");
            }

            return EndPointResponse<IEnumerable<GetProductsByBrandIdResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
