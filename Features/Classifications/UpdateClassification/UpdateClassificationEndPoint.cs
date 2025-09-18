using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Classifications.UpdateClassification.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Classifications.UpdateClassification
{
    public class UpdateClassificationEndPoint : EndpointBase<UpdateClassificationRequestViewModel, UpdateClassificationResponseViewModel>
    {
        public UpdateClassificationEndPoint(EndpointBaseParameters<UpdateClassificationRequestViewModel> parameters) : base(parameters) { }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditClassification })]
        public async Task<EndPointResponse<UpdateClassificationResponseViewModel>> UpdateClassification(UpdateClassificationRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<UpdateClassificationCommand>());
            if (result.IsSuccess)
                return EndPointResponse<UpdateClassificationResponseViewModel>.Success(new UpdateClassificationResponseViewModel(), "Classification Updated successfully.");
            else
                return EndPointResponse<UpdateClassificationResponseViewModel>.Failure(result.ErrorCode);
        }
    }

}
