using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Classifications.CreateClassification.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Classifications.CreateClassification
{
    public class CreateClassificationEndPoint : EndpointBase<CreateClassificationRequestViewModel, CreateClassificationResponseViewModel>
    {
        public CreateClassificationEndPoint(EndpointBaseParameters<CreateClassificationRequestViewModel> parameters) : base(parameters) { }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreateClassification })]
        public async Task<EndPointResponse<CreateClassificationResponseViewModel>> AddClassification(CreateClassificationRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<CreateClassificationCommand>());
            if (result.IsSuccess)
                return EndPointResponse<CreateClassificationResponseViewModel>.Success(new CreateClassificationResponseViewModel(), "Classification Added successfully.");
            else
                return EndPointResponse<CreateClassificationResponseViewModel>.Failure(result.ErrorCode);
        }
    }

}
