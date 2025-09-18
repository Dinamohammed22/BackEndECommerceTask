using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.ClientGroups.DeleteClientGroup.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ClientGroups.DeleteClientGroup
{
    public class DeleteClientGroupEndPoint : EndpointBase<DeleteClientGroupRequestViewModel, DeleteClientGroupResponseViewModel>
    {
        public DeleteClientGroupEndPoint(EndpointBaseParameters<DeleteClientGroupRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeleteClientGroup })]
        public async Task<EndPointResponse<DeleteClientGroupResponseViewModel>> Delete(DeleteClientGroupRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<DeleteClientGroupCommand>());

            if (result.IsSuccess)
                return EndPointResponse<DeleteClientGroupResponseViewModel>.Success(new DeleteClientGroupResponseViewModel(), "ClientGroup Deleted successfully.");
            else
                return EndPointResponse<DeleteClientGroupResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
