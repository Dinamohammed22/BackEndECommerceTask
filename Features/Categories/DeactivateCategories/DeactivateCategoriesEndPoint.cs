using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Categories.ActivateCategories.Commands;
using KOG.ECommerce.Features.Categories.ActivateCategories;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Categories.DeactivateCategories.Commands;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Categories.DeactivateCategories
{
    public class DeactivateCategoriesEndPoint : EndpointBase<DeactivateCategoriesRequestViewModel, DeactivateCategoriesResponseViewModel>
    {
        public DeactivateCategoriesEndPoint(EndpointBaseParameters<DeactivateCategoriesRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeactiveCategory })]
        public async Task<EndPointResponse<DeactivateCategoriesResponseViewModel>> DeactivateCategory(DeactivateCategoriesRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeactivateCategoriesCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeactivateCategoriesResponseViewModel>.Success(new DeactivateCategoriesResponseViewModel(), "Category Deactivated Successfully");
            else
                return EndPointResponse<DeactivateCategoriesResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
