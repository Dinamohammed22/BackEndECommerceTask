using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Categories.ActivateCategories.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Categories.ActivateCategories
{
    public class ActivateCategoriesEndPoint : EndpointBase<ActivateCategoriesRequestViewModel, ActivateCategoriesResponseViewModel>
    {
        public ActivateCategoriesEndPoint(EndpointBaseParameters<ActivateCategoriesRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ActiveCategory })]
        public async Task<EndPointResponse<ActivateCategoriesResponseViewModel>> ActiveCategory(ActivateCategoriesRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ActivateCategoriesCommand>());
            if (result.IsSuccess)
                return EndPointResponse<ActivateCategoriesResponseViewModel>.Success(new ActivateCategoriesResponseViewModel(), "Category Activated Successfully");
            else
                return EndPointResponse<ActivateCategoriesResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
