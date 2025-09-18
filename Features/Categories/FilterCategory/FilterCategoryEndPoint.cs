using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Categories.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Categories.FilterCategory
{
    public class FilterCategoryEndPoint : EndpointBase<FilterCategoryRequestViewModel, FilterCategoryResponseViewModel>
    {
        public FilterCategoryEndPoint(EndpointBaseParameters<FilterCategoryRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.FilterCategory })]
        public async Task<ActionResult<EndPointResponse<IEnumerable<FilterCategoryResponseViewModel>>>> FilterCategories(
           [FromQuery] FilterCategoryRequestViewModel? filter)
        {
            var result = await _mediator.Send(filter.MapOne<FilterCategoryQuery>());
            IEnumerable<FilterCategoryResponseViewModel> response = result.Data.MapList<FilterCategoryResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {
                return EndPointResponse<IEnumerable<FilterCategoryResponseViewModel>>
                    .Success(response, "Categories filtered successfully.");
            }

            return EndPointResponse<IEnumerable<FilterCategoryResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
