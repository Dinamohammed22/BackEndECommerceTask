using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.RoleFeatures.GetFeaturesByRoleId;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using System.Collections.Generic;

namespace KOG.ECommerce.Features.RoleFeatures.GetFeaturesByRoleId
{
    public class GetFeaturesByRoleIdEndPoint : EndpointBase<GetFeaturesByRoleIdRequestViewModel, GetFeaturesByRoleIdResponseViewModel>
    {
        public GetFeaturesByRoleIdEndPoint(EndpointBaseParameters<GetFeaturesByRoleIdRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetModulesbyRoleID })]
        public async Task<EndPointResponse<GetFeaturesByRoleIdResponseViewModel>> GetModulesByRoleId([FromQuery] GetFeaturesByRoleIdRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<GetFeaturesByRoleIdQuery>());

            var response = result.Data.MapOne<GetFeaturesByRoleIdResponseViewModel>();

            return EndPointResponse<GetFeaturesByRoleIdResponseViewModel>
                .Success(response, "Features retrieved successfully.");
        }
    }
}
