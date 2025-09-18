using Microsoft.AspNetCore.Mvc;
using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Clients.AddClientImage.Command;

namespace KOG.ECommerce.Features.Clients.AddClientImage
{
    public class AddClientImageEndpoint : EndpointBase<AddClientImageRequestViewModel, AddClientImageResponseViewModel>
    {
        public AddClientImageEndpoint(EndpointBaseParameters<AddClientImageRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.AddClientImage })]
        public async Task<EndPointResponse<AddClientImageResponseViewModel>> AddClientImage(AddClientImageRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<AddClientImageCommand>());
            if (result.IsSuccess)
                return EndPointResponse<AddClientImageResponseViewModel>.Success(new AddClientImageResponseViewModel(), "Add client Image Successfully");
            else
                return EndPointResponse<AddClientImageResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
