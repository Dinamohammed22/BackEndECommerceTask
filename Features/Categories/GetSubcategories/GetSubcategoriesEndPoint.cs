using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Categories.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Categories.GetSubcategories
{
    public class GetSubcategoriesEndPoint : EndpointBase<GetSubcategoriesRequestViewModel, GetSubcategoriesResponseViewModel>
    {
        public GetSubcategoriesEndPoint(EndpointBaseParameters<GetSubcategoriesRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetSubcategories })]
        public async Task<ActionResult<EndPointResponse<IEnumerable<GetSubcategoriesResponseViewModel>>>> GetSubcategories(
            [FromQuery] GetSubcategoriesRequestViewModel? viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<GetSubcategoriesQuery>());

            if (result.IsSuccess && result.Data != null)
            {
                IEnumerable<GetSubcategoriesResponseViewModel> response = result.Data.MapList<GetSubcategoriesResponseViewModel>();
                return EndPointResponse<IEnumerable<GetSubcategoriesResponseViewModel>>
                    .Success(response, "Get Subcategories successfully.");
            }

            return EndPointResponse<IEnumerable<GetSubcategoriesResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
