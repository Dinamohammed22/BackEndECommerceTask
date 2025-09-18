using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.GetProductsByCategoryId
{
    public class GetProductsByCategoryIdEndpoint : EndpointBase<GetProductsByCategoryIdRequest, GetProductsByCategoryIdResponse>
    {
        public GetProductsByCategoryIdEndpoint(EndpointBaseParameters<GetProductsByCategoryIdRequest> dependencyCollection) : base(dependencyCollection)
        {
        }


        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetProductsByCategoryId })]

        public async Task<EndPointResponse<PagingViewModel<GetProductsByCategoryIdResponse>>> GetProductsByCategoryId([FromQuery] GetProductsByCategoryIdRequest request)
        {
            var result = await _mediator.Send(request.MapOne<GetProductsByCategoryIdQuery>());
            var response = result.Data.MapPage<GetProductsByCategoryIdDTO, GetProductsByCategoryIdResponse>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetProductsByCategoryIdResponse>>.Success(response, "Get all products successfully.");
            else
                return EndPointResponse<PagingViewModel<GetProductsByCategoryIdResponse>>.Failure(result.ErrorCode);


        }
    }
}
