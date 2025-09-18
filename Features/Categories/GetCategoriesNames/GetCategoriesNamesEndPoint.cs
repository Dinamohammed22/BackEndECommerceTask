using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Categories.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Categories.GetCategoriesNames
{
    public class GetCategoriesNamesEndPoint : EndpointBase<GetCategoriesNamesRequestViewModel, GetCategoriesNamesResponseViewModel>
    {
        public GetCategoriesNamesEndPoint(EndpointBaseParameters<GetCategoriesNamesRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetCategoriesNames })]
        public async Task<ActionResult<EndPointResponse<IEnumerable<GetCategoriesNamesResponseViewModel>>>> GetCategoriesNames()
        {
            var result = await _mediator.Send(new GetCategoriesNamesQuery());
           var response = result.Data.MapList<GetCategoriesNamesResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {
                return EndPointResponse<IEnumerable<GetCategoriesNamesResponseViewModel>>
                    .Success(response, "Categories Names and Products Number get successfully.");
            }

            return EndPointResponse<IEnumerable<GetCategoriesNamesResponseViewModel>>
                .Failure(result.ErrorCode);
        }
    }
}
