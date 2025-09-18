using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Products.GetProductById.Orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.GetProductById
{
    public class GetProductByIDEndPoint : EndpointBase<GetProductByIDRequestViewModel, GetProductByIDResponseViewModel>
    {
        public GetProductByIDEndPoint(EndpointBaseParameters<GetProductByIDRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetProductById })]
        public async Task<EndPointResponse<GetProductByIDResponseViewModel>> GetByID([FromQuery] GetProductByIDRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<GetProductByIDWithMediaOrchestrator>());

            var response = result.Data.MapOne<GetProductByIDResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<GetProductByIDResponseViewModel>.Success(response, "Get Product successfully");
            else
                return EndPointResponse<GetProductByIDResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
