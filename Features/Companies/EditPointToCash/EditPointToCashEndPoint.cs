using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Companies.EditPointToCash.Commands;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Companies.EditPointToCash
{
    public class EditPointToCashEndPoint : EndpointBase<EditPointToCashRequestViewModel, EditPointToCashResponseViewModel>
    {
        public EditPointToCashEndPoint(EndpointBaseParameters<EditPointToCashRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditPointToCash })]
        public async Task<EndPointResponse<EditPointToCashResponseViewModel>> EditPointToCash(EditPointToCashRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<EditPointToCashCommand>());

            if (result.IsSuccess)
                return EndPointResponse<EditPointToCashResponseViewModel>.Success(new EditPointToCashResponseViewModel(), "Point To Cash Updated successfully");
            else
                return EndPointResponse<EditPointToCashResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
