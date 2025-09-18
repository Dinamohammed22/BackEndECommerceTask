using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Clients.GetClientById
{
    public class GetClientByIdEndPoint : EndpointBase<GetClientByIdRequestViewModel, GetClientByIdResponseViewModel>
    {
        public GetClientByIdEndPoint(EndpointBaseParameters<GetClientByIdRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetClientById })]
        public async Task<EndPointResponse<GetClientByIdResponseViewModel>> GetClientById([FromQuery]GetClientByIdRequestViewModel Request)
        {
            var result = await _mediator.Send(Request.MapOne<GetClientByIdQuery>());
            var response = result.Data.MapOne<GetClientByIdResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<GetClientByIdResponseViewModel>.Success(response, "client retrived succefully");
            else
                return EndPointResponse<GetClientByIdResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
