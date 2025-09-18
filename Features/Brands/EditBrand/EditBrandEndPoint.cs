using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Brands.EditBrand.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Brands.EditBrand
{
    public class EditBrandEndPoint : EndpointBase<EditBrandRequestViewModel, EditBrandResponseViewModel>
    {
        public EditBrandEndPoint(EndpointBaseParameters<EditBrandRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditBrand })]
        public async Task<EndPointResponse<EditBrandResponseViewModel>> EditBrand(EditBrandRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<EditBrandOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<EditBrandResponseViewModel>.Success(new EditBrandResponseViewModel(), "Brand Updated Successfully");
            else
                return EndPointResponse<EditBrandResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
