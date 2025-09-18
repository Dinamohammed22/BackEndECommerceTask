using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Categories.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Categories.SelectSubcategoryList
{
    public class SelectSubcategoryListEndPoint : EndpointBase<SelectSubcategoryListRequestViewModel, SelectSubcategoryListResponseViewModel>
    {
        public SelectSubcategoryListEndPoint(EndpointBaseParameters<SelectSubcategoryListRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature. SelectSubcategoryList })]
        public async Task<ActionResult<EndPointResponse<IEnumerable<SelectSubcategoryListResponseViewModel>>>> SelectSubcategoryList([FromQuery] SelectSubcategoryListRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<SelectSubcategoryListQuery>());
            IEnumerable<SelectSubcategoryListResponseViewModel> response = result.Data.MapList<SelectSubcategoryListResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {
                return EndPointResponse<IEnumerable<SelectSubcategoryListResponseViewModel>>
                    .Success(response, "Select SubcategoryList successfully.");
            }

            return EndPointResponse<IEnumerable<SelectSubcategoryListResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
