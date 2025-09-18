using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.RoleModules.GetModulesByRoleId.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KOG.ECommerce.Features.RoleModules.GetModulesByRoleId
{
    public class GetModulesByRoleIdEndPoint : EndpointBase<GetModulesByRoleIdRequestViewModel, GetModulesByRoleIdResponseViewModel>
    {
        public GetModulesByRoleIdEndPoint(EndpointBaseParameters<GetModulesByRoleIdRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetModulesbyRoleID })]
        public async Task<EndPointResponse<IEnumerable<GetModulesByRoleIdResponseViewModel>>> GetModulesByRoleId([FromQuery]GetModulesByRoleIdRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<GetModulesByRoleIdQuery>());

            // Map the results to response view models
            IEnumerable<GetModulesByRoleIdResponseViewModel> response = result.Data.MapList<GetModulesByRoleIdResponseViewModel>();

            return EndPointResponse<IEnumerable<GetModulesByRoleIdResponseViewModel>>
                .Success(response, "Modules retrieved successfully.");
        }
    }
}
