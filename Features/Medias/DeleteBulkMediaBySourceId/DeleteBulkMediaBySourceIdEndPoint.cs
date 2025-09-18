using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Medias.DeleteBulkMediaBySourceId.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Medias.DeleteBulkMediaBySourceId;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Medias.DeleteMedia
{
    public class DeleteBulkMediaBySourceIdEndPoint : EndpointBase<DeleteBulkMediaBySourceIdRequestViewModel, DeleteBulkMediaBySourceIdResponseViewModel>
    {
        public DeleteBulkMediaBySourceIdEndPoint(EndpointBaseParameters<DeleteBulkMediaBySourceIdRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeleteBulkMediaBySourceId })]
        public async Task<EndPointResponse<DeleteBulkMediaBySourceIdResponseViewModel>> DeleteBulkMediaBySourceId(DeleteBulkMediaBySourceIdRequestViewModel viewModel)

        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            // Send the command to upload the files
            var result = await _mediator.Send(viewModel.MapOne<DeleteBulkMediaBySourceIdCommand>());

            if (result.IsSuccess)
                return EndPointResponse<DeleteBulkMediaBySourceIdResponseViewModel>.Success(new DeleteBulkMediaBySourceIdResponseViewModel(), "Media files Deleted successfully");
            else
                return EndPointResponse<DeleteBulkMediaBySourceIdResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
