using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Products.DeleteProduct.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Products.DeleteProduct
{
    public class DeleteProductEndPoint:EndpointBase<DeleteProductRequestViewModel,DeleteProductResponseViewModel>
    {
        public DeleteProductEndPoint(EndpointBaseParameters<DeleteProductRequestViewModel> parameters) : base(parameters) { }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeleteProduct })]
        public async Task<EndPointResponse<DeleteProductResponseViewModel>> DeleteProduct(DeleteProductRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeleteProductCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeleteProductResponseViewModel>.Success(new DeleteProductResponseViewModel(), "Product Deleted successfully");
            else
                return EndPointResponse<DeleteProductResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
