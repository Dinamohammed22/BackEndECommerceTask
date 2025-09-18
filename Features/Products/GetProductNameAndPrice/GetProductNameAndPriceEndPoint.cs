using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Products.GetProductNameAndPrice
{
    public class GetProductNameAndPriceEndPoint : EndpointBase<GetProductNameAndPriceRequestViewModel, GetProductNameAndPriceResponseViewModel>
    {
        public GetProductNameAndPriceEndPoint(EndpointBaseParameters<GetProductNameAndPriceRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetProductNameAndPrice })]
        public async Task<EndPointResponse<GetProductNameAndPriceResponseViewModel>> GetProductNameAndPrice([FromQuery] GetProductNameAndPriceRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<GetProductNameAndPriceQuery>());

            var response = result.Data.MapOne<GetProductNameAndPriceResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<GetProductNameAndPriceResponseViewModel>.Success(response, "Get Product successfully");
            else
                return EndPointResponse<GetProductNameAndPriceResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
