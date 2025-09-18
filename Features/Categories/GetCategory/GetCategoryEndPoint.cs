// CategoryFilterIndexEndpoint.cs
using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Categories.GetCategory;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Features.Common.Categories.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Categories.CategoryFilterIndex
{
    public class CategoryFilterIndexEndpoint : EndpointBase<GetCategoryRequestViewModel, GetCategoryResponseViewModel>
    {
        public CategoryFilterIndexEndpoint(EndpointBaseParameters<GetCategoryRequestViewModel> dependencyCollection)
            : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetCategorylist })]
        public async Task<ActionResult<EndPointResponse<IEnumerable<GetCategoryResponseViewModel>>>> FilterCategories(
            [FromQuery] GetCategoryRequestViewModel? filter)
        {
            var result = await _mediator.Send(filter.MapOne<GetCategoryQuery>());
            IEnumerable<GetCategoryResponseViewModel> response = result.Data.MapList<GetCategoryResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {
                return EndPointResponse<IEnumerable<GetCategoryResponseViewModel>>
                    .Success(response, "Categories filtered successfully.");
            }

            return EndPointResponse<IEnumerable<GetCategoryResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
