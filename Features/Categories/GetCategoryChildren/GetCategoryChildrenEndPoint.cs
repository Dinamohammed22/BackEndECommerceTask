using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Categories.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Categories.GetCategoryChildren
{
    public class GetCategoryChildrenEndPoint : EndpointBase<GetCategoryChildrenRequestViewModel, GetCategoryChildrenResponseViewModel>
    {
        public GetCategoryChildrenEndPoint(EndpointBaseParameters<GetCategoryChildrenRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetCategoryChildren })]
        public async Task<EndPointResponse<IEnumerable<GetCategoryChildrenResponseViewModel>>> GetCategoryChildren([FromQuery] GetCategoryChildrenRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetCategoryChildrenQuery>());

            var response = result.Data.MapList<GetCategoryChildrenResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<IEnumerable<GetCategoryChildrenResponseViewModel>>.Success(response, "Get Category childeren successfully.");
            else
                return EndPointResponse<IEnumerable<GetCategoryChildrenResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
