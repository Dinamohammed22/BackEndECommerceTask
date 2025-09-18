using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.ClientGroups.BulkDeleteClientGroups.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ClientGroups.BulkDeleteClientGroups
{
    public class BulkDeleteClientGroupsEndpoint : EndpointBase<BulkDeleteClientGroupsRequestViewModel, BulkDeleteClientGroupsResponseViewModel>
    {
        public BulkDeleteClientGroupsEndpoint(EndpointBaseParameters<BulkDeleteClientGroupsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeleteClientGroups })]
        public async Task<EndPointResponse<BulkDeleteClientGroupsResponseViewModel>> BulkDeleteClientGroups(BulkDeleteClientGroupsRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<BulkDeleteClientGroupsCommand>());

            if (result.IsSuccess)
                return EndPointResponse<BulkDeleteClientGroupsResponseViewModel>.Success(new BulkDeleteClientGroupsResponseViewModel(), "ClientGroups Deleted successfully.");
            else
                return EndPointResponse<BulkDeleteClientGroupsResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
