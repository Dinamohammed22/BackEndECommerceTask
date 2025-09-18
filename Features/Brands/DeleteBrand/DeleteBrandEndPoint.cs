using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Brands.DeleteBrand.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Brands.DeleteBrand
{
    public class DeleteBrandEndPoint : EndpointBase<DeleteBrandRequestViewModel, DeleteBrandResponseViewModel>
    {
        public DeleteBrandEndPoint(EndpointBaseParameters<DeleteBrandRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeleteBrand })]
        public async Task<EndPointResponse<DeleteBrandResponseViewModel>> DeleteBrand(DeleteBrandRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeleteBrandCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeleteBrandResponseViewModel>.Success(new DeleteBrandResponseViewModel(), "Brand Deleted Successfully");
            else
                return EndPointResponse<DeleteBrandResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
