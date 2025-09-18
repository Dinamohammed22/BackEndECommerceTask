using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Products.CreateProduct.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Products.CreateProduct
{
    public class CreateProductEndPoint : EndpointBase<CreateProductRequestViewModel, CreateProductResponseViewModel>
    {
        public CreateProductEndPoint(EndpointBaseParameters<CreateProductRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreateProduct })]
        public async Task<EndPointResponse<CreateProductResponseViewModel>> AddProduct(CreateProductRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<CreateProductOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<CreateProductResponseViewModel>.Success(new CreateProductResponseViewModel(), "Product Added successfully");
            else
                return EndPointResponse<CreateProductResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
