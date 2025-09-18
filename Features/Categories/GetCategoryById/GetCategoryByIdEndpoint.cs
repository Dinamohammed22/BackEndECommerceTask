using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Categories.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Categories.GetCategoryById
{
    public class GetCategoryByIdEndpoint : EndpointBase<GetCategoryByIdRequestViewModel, GetCategoryByIdResponseViewModel>
    {
        public GetCategoryByIdEndpoint(EndpointBaseParameters<GetCategoryByIdRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetCategoryById })]
        public async Task<EndPointResponse<GetCategoryByIdResponseViewModel>> GetByID([FromQuery] GetCategoryByIdRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetCategoryByIdQuery>());

            GetCategoryByIdResponseViewModel response = result.Data.MapOne<GetCategoryByIdResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<GetCategoryByIdResponseViewModel>.Success(response, "Get Category successfully.");
            else
                return EndPointResponse<GetCategoryByIdResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
