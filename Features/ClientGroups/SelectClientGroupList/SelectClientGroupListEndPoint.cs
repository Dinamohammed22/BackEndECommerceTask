using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.ClientGroups.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.ClientGroups.SelectClientGroupList
{
    public class SelectClientGroupListEndPoint : EndpointBase<SelectClientGroupListRequestViewModel, SelectClientGroupListResponseViewModel>
    {
        public SelectClientGroupListEndPoint(EndpointBaseParameters<SelectClientGroupListRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.SelectClientGroupList })]
        public async Task<EndPointResponse<IEnumerable<SelectClientGroupListResponseViewModel>>> SelectClientGroupList()
        {


            var result = await _mediator.Send(new SelectClientGroupListQuery());

            var response = result.Data.MapList<SelectClientGroupListResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<IEnumerable<SelectClientGroupListResponseViewModel>>.Success(response, "Select ClientGroup List successfully.");
            else
                return EndPointResponse<IEnumerable<SelectClientGroupListResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
