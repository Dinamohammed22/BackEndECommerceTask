using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Categories.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Categories.SelectCategoryList
{
    public class SelectCategoryListEndpoint : EndpointBase<SelectCategoryListRequestViewModel, SelectCategoryListResponseViewModel>
    {
        public SelectCategoryListEndpoint(EndpointBaseParameters<SelectCategoryListRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature. SelectCategoryList })]
        public async Task<ActionResult<EndPointResponse<IEnumerable<SelectCategoryListResponseViewModel>>>> GetCategories()
        {
            var result = await _mediator.Send(new SelectCategoryListQuery());
            IEnumerable<SelectCategoryListResponseViewModel> response = result.Data.MapList<SelectCategoryListResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {
                return EndPointResponse<IEnumerable<SelectCategoryListResponseViewModel>>
                    .Success(response, "Categories filtered successfully.");
            }

            return EndPointResponse<IEnumerable<SelectCategoryListResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
