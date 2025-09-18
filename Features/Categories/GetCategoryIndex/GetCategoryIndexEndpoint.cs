using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Features.Common.Categories.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Categories.GetCategoryIndex
{
    public class GetCategoryIndexEndpoint : EndpointBase<GetCategoryIndexRequestViewModel, GetCategoryIndexResponseViewModel>
    {
        public GetCategoryIndexEndpoint(EndpointBaseParameters<GetCategoryIndexRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetCategoryIndex })]
        public async Task<ActionResult<EndPointResponse<PagingViewModel<GetCategoryIndexResponseViewModel>>>> Get(
         [FromQuery] GetCategoryIndexRequestViewModel? filter)
        {

            var result = await _mediator.Send(filter.MapOne<GetAllCategoryIndexQuery>());
            var response = result.Data.MapPage<GetAllCategoryIndexDTO, GetCategoryIndexResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<PagingViewModel<GetCategoryIndexResponseViewModel>>
                    .Success(response, "Categories filtered successfully.");
            }

            return EndPointResponse<PagingViewModel<GetCategoryIndexResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
