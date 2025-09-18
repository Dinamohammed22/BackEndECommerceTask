using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.ShippingAddresses.EditShippingAddress.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ShippingAddresses.EditShippingAddress
{
    public class EditShippingAddressEndPoint : EndpointBase<EditShippingAddressRequestViewModel, EditShippingAddressResponseViewModel>
    {
        public EditShippingAddressEndPoint(EndpointBaseParameters<EditShippingAddressRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditShippingAddress })]
        public async Task<EndPointResponse<EditShippingAddressResponseViewModel>> Put(EditShippingAddressRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<EditShippingAddressCommand>());
            if (result.IsSuccess)
            {
                return EndPointResponse<EditShippingAddressResponseViewModel>.Success(new EditShippingAddressResponseViewModel(), "ShippingAddress Updated successfully");
            }
            return EndPointResponse<EditShippingAddressResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
