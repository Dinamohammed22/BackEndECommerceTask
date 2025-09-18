using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.ShippingAddresses.RejectShippingAddress.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.ShippingAddresses.RejectShippingAddress
{
    public class RejectShippingAddressEndpoint : EndpointBase<RejectShippingAddressRequestViewModel, RejectShippingAddressResponseViewModel>
    {
        public RejectShippingAddressEndpoint(EndpointBaseParameters<RejectShippingAddressRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature. RejectShippingAddress })]
        public async Task<EndPointResponse<RejectShippingAddressResponseViewModel>> RejectShippingAddress(RejectShippingAddressRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<RejectShippingAddressCommand>());
            if (result.IsSuccess)
                return EndPointResponse<RejectShippingAddressResponseViewModel>.Success(new RejectShippingAddressResponseViewModel(), " Shipping Address Rejected Successfully");
            else
                return EndPointResponse<RejectShippingAddressResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
