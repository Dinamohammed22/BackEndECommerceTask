using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.ShippingAddresses.CreateShippingAddress.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ShippingAddresses.CreateShippingAddress
{
    public class CreateShippingAddressEndPoint : EndpointBase<CreateShippingAddressRequestViewModel, CreateShippingAddressResponseViewModel>
    {
        public CreateShippingAddressEndPoint(EndpointBaseParameters<CreateShippingAddressRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreateShippingAddress })]
        public async Task<EndPointResponse<CreateShippingAddressResponseViewModel>> Post(CreateShippingAddressRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<CreateShippingAddressCommand>());
            if (result.IsSuccess)
            {
                return EndPointResponse<CreateShippingAddressResponseViewModel>.Success(new CreateShippingAddressResponseViewModel(), "ShippingAddress Added successfully");
            }
            return EndPointResponse<CreateShippingAddressResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
