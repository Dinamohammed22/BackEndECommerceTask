using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Categories.EditCategory.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Categories.EditCategory
{
    public class EditCategoryEndPoint : EndpointBase<EditCategoryRequestViewModel, EditCategoryResponseViewModel>
    {
        public EditCategoryEndPoint(EndpointBaseParameters<EditCategoryRequestViewModel> dependencyCollection)
            : base(dependencyCollection)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditCategory })]
        public async Task<EndPointResponse<EditCategoryResponseViewModel>> Put(EditCategoryRequestViewModel viewModel)
        {

            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<EditCategoryOrchestrator>());

            if (result.IsSuccess)
            {
                return EndPointResponse<EditCategoryResponseViewModel>.Success(
                    new EditCategoryResponseViewModel(), "Category Updated Successfully");
            }

            return EndPointResponse<EditCategoryResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}