using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.GetListProduct
{
    public class GetProductEndPoint : EndpointBase<GetProductRequestViewModel, GetProductResponseViewModel>
    {
        public GetProductEndPoint(EndpointBaseParameters<GetProductRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetProductList })]
        public async Task<EndPointResponse<IQueryable<GetProductResponseViewModel>>> GetList()
        {

            var result = await _mediator.Send(new GetProductQuery());

            var response = result.Data.Map<GetProductResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<IQueryable<GetProductResponseViewModel>>.Success(response, "Product filtered successfully");
            else
                return EndPointResponse<IQueryable<GetProductResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
