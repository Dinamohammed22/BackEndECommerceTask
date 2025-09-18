using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Categories.DeleteCategory.Command;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Categories.DeleteCategory
{
    public class DeleteCategoryEndPoint : EndpointBase<DeleteCategoryRequestViewModel, DeleteCategoryResponseViewModel>
    {
        public DeleteCategoryEndPoint(EndpointBaseParameters<DeleteCategoryRequestViewModel> parameters) : base(parameters) { }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeleteCategory })]
        public async Task<EndPointResponse<DeleteCategoryResponseViewModel>> DeleteCategory(DeleteCategoryRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeleteCategoryCammand>());
            if (result.IsSuccess)
                return EndPointResponse<DeleteCategoryResponseViewModel>.Success(new DeleteCategoryResponseViewModel(), "Category Deleted Successfully");
            else
                return EndPointResponse<DeleteCategoryResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
