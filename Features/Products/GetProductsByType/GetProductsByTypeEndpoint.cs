using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.GetProductsByType
{
    public class GetProductsByTypeEndpoint : EndpointBase<GetProductsByTypeRequestViewModel, GetProductsByTypeResponseViewModel>
    {
        public GetProductsByTypeEndpoint(EndpointBaseParameters<GetProductsByTypeRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature. GetProductsByType })]
        public async Task<EndPointResponse<IEnumerable<GetProductsByTypeResponseViewModel>>> GetProductsByType([FromQuery] GetProductsByTypeRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetProductsByTypeQuery>());

            var response = result.Data.MapList<GetProductsByTypeResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<IEnumerable<GetProductsByTypeResponseViewModel>>
                    .Success(response, "Get Products successfully.");
            }

            return EndPointResponse<IEnumerable<GetProductsByTypeResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
