using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Brands.CreateBrand.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Brands.CreateBrand
{
    public class CreateBrandEndPoint : EndpointBase<CreateBrandRequestViewModel, CreateBrandResponseViewModel>
    {
        public CreateBrandEndPoint(EndpointBaseParameters<CreateBrandRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreateBrand })]
        public async Task<EndPointResponse<CreateBrandResponseViewModel>> AddBrand(CreateBrandRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<CreateBrandOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<CreateBrandResponseViewModel>.Success(new CreateBrandResponseViewModel(), "Brand Added Successfully");
            else
                return EndPointResponse<CreateBrandResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
