using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.CartProducts.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.CartProducts.GetAllProductAtCart
{
    public class GetAllProductAtCartEndpoint : EndpointBase<GetAllProductAtCartRequestViewModel, GetAllProductAtCartResponseViewModel>
    {
        public GetAllProductAtCartEndpoint(EndpointBaseParameters<GetAllProductAtCartRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllProductAtCart })]
        public async Task<EndPointResponse<IEnumerable<GetAllProductAtCartResponseViewModel>>> GetAll()
        {

            var result = await _mediator.Send(new GetAllProductAtCartQuery());

            IEnumerable<GetAllProductAtCartResponseViewModel> response = result.Data.MapList<GetAllProductAtCartResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<IEnumerable<GetAllProductAtCartResponseViewModel>>.Success(response, result.Message);
            else
                return EndPointResponse<IEnumerable<GetAllProductAtCartResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
