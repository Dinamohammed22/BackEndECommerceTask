using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Pages.CreatePage.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Pages.CreatePage
{
    public class CreatePageEndPoint : EndpointBase<CreatePageRequestViewModel, CreatePageResponseViewModel>
    {
        public CreatePageEndPoint(EndpointBaseParameters<CreatePageRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreatePage })]
        public async Task<EndPointResponse<CreatePageResponseViewModel>> Post(CreatePageRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<CreatePageCommand>());

            if (result.IsSuccess)
                return EndPointResponse<CreatePageResponseViewModel>.Success(new CreatePageResponseViewModel(), "Page Added successfully");
            else
                return EndPointResponse<CreatePageResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
