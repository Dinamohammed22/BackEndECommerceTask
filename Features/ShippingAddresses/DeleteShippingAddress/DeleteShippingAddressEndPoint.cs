using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.ShippingAddresses.DeleteShippingAddress.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ShippingAddresses.DeleteShippingAddress
{
    public class DeleteShippingAddressEndPoint : EndpointBase<DeleteShippingAddressRequestViewModel, DeleteShippingAddressResponseViewModel>
    {
        public DeleteShippingAddressEndPoint(EndpointBaseParameters<DeleteShippingAddressRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeleteShippingAddress })]
        public async Task<EndPointResponse<DeleteShippingAddressResponseViewModel>> Delete(DeleteShippingAddressRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeleteShippingAddressCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeleteShippingAddressResponseViewModel>.Success(new DeleteShippingAddressResponseViewModel(), "ShippingAddress Deleted successfully");
            else
                return EndPointResponse<DeleteShippingAddressResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
