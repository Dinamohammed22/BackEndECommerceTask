using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.ClientGroups.EditClientGroup.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.ClientGroups.EditClientGroup
{
    public class EditClientGroupEndPoint : EndpointBase<EditClientGroupRequestViewModel, EditClientGroupResponseViewModel>
    {
        public EditClientGroupEndPoint(EndpointBaseParameters<EditClientGroupRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditClientGroup})]
        public async Task<EndPointResponse<EditClientGroupResponseViewModel>> Put(EditClientGroupRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<EditClientGroupCommand>());

            if (result.IsSuccess)
                return EndPointResponse<EditClientGroupResponseViewModel>.Success(new EditClientGroupResponseViewModel(), "ClientGroup Updated successfully.");
            else
                return EndPointResponse<EditClientGroupResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
