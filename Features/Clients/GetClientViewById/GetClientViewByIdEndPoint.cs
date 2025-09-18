using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Clients.GetClientViewById
{
    public class GetClientViewByIdEndPoint : EndpointBase<GetClientViewByIdRequestViewModel, GetClientViewByIdResponseViewModel>
    {
        public GetClientViewByIdEndPoint(EndpointBaseParameters<GetClientViewByIdRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetClientViewById })]
        public async Task<EndPointResponse<GetClientViewByIdResponseViewModel>> GetClientViewById([FromQuery] GetClientViewByIdRequestViewModel Request)
        {
            var result = await _mediator.Send(Request.MapOne<GetClientViewByIdQuery>());
            var response = result.Data.MapOne<GetClientViewByIdResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<GetClientViewByIdResponseViewModel>.Success(response, "client retrived succefully");
            else
                return EndPointResponse<GetClientViewByIdResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
