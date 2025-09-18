using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.ShippingAddresses.ApproveShippingAddress.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ShippingAddresses.ApproveShippingAddress
{
    public class ApproveShippingAddressEndpoint : EndpointBase<ApproveShippingAddressRequestViewModel, ApproveShippingAddressResponseViewModel>
    {
        public ApproveShippingAddressEndpoint(EndpointBaseParameters<ApproveShippingAddressRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ApproveShippingAddress })]
        public async Task<EndPointResponse<ApproveShippingAddressResponseViewModel>> ApproveShippingAddress(ApproveShippingAddressRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ApproveShippingAddressCommand>());
            if (result.IsSuccess)
                return EndPointResponse<ApproveShippingAddressResponseViewModel>.Success(new ApproveShippingAddressResponseViewModel(), " Shipping Address Approved Successfully");
            else
                return EndPointResponse<ApproveShippingAddressResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
