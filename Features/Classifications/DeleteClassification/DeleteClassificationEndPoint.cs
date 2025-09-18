using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Classifications.DeleteClassification.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Classifications.DeleteClassification
{
    public class DeleteClassificationEndPoint : EndpointBase<DeleteClassificationRequestViewModel, DeleteClassificationResponseViewModel>
    {
        public DeleteClassificationEndPoint(EndpointBaseParameters<DeleteClassificationRequestViewModel> parameters) : base(parameters) { }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeleteClassification })]
        public async Task<EndPointResponse<DeleteClassificationResponseViewModel>> DeleteClassification(DeleteClassificationRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeleteClassificationCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeleteClassificationResponseViewModel>.Success(new DeleteClassificationResponseViewModel(), "Classification Deleted successfully.");
            else
                return EndPointResponse<DeleteClassificationResponseViewModel>.Failure(result.ErrorCode);
        }
    }

}
