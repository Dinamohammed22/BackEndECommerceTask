using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Categories.CreateCategory.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Categories.CreateCategory
{
    public class CreateCategoryEndPoint : EndpointBase<CreateCategoryRequestViewModel, CreateCategoryResponseViewModel>
    {
        public CreateCategoryEndPoint(EndpointBaseParameters<CreateCategoryRequestViewModel> parameters) : base(parameters) { }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreateCategory })]
        public async Task<EndPointResponse<CreateCategoryResponseViewModel>> AddCategory(CreateCategoryRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<CreateCategoryOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<CreateCategoryResponseViewModel>.Success(result.Data.MapOne<CreateCategoryResponseViewModel>(), "Category Added Successfully");
            else
                return EndPointResponse<CreateCategoryResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
