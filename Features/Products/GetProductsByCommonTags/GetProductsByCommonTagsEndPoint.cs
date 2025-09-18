using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.GetProductsByCommonTags
{
    public class GetProductsByCommonTagsEndPoint : EndpointBase<GetProductsByCommonTagsRequestViewModel, GetProductsByCommonTagsResponseViewModel>
    {
        public GetProductsByCommonTagsEndPoint(EndpointBaseParameters<GetProductsByCommonTagsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }


        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetProductsByCommonTags })]
        public async Task<EndPointResponse<IEnumerable<GetProductsByCommonTagsResponseViewModel>>> GetProductsByCommonTags([FromQuery] GetProductsByCommonTagsRequestViewModel request)
        {
            var result = await _mediator.Send(request.MapOne<GetProductsByCommonTagsQuery>());


            if (result.IsSuccess && result.Data != null)
            {
                var response = result.Data.MapList<GetProductsByCommonTagsResponseViewModel>();
                return EndPointResponse<IEnumerable<GetProductsByCommonTagsResponseViewModel>>.Success(response, "Get all products successfully.");
            }
            else
                return EndPointResponse<IEnumerable<GetProductsByCommonTagsResponseViewModel>>.Failure(result.ErrorCode);


        }
    }
}
