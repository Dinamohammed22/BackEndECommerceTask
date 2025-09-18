using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.ShippingAddresses.SetDefaultShippingAddress.Commands;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.ShippingAddresses.SetDefaultShippingAddress
{
    public class SetDefaultShippingAddressEndPoint : EndpointBase<SetDefaultShippingAddressRequestViewModel, SetDefaultShippingAddressResponseViewModel>
    {
        public SetDefaultShippingAddressEndPoint(EndpointBaseParameters<SetDefaultShippingAddressRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.SetDefaultShippingAddress })]
        public async Task<EndPointResponse<SetDefaultShippingAddressResponseViewModel>> SetDefaultShippingAddress(SetDefaultShippingAddressRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<SetDefaultShippingAddressCommand>());
            if (result.IsSuccess)
                return EndPointResponse<SetDefaultShippingAddressResponseViewModel>.Success(new SetDefaultShippingAddressResponseViewModel(), "Set Default ShippingAddress Successfully");
            else
                return EndPointResponse<SetDefaultShippingAddressResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
