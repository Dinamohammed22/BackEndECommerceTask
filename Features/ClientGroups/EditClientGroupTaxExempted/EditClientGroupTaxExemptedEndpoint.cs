using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.ClientGroups.EditClientGroupTaxExempted.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ClientGroups.EditClientGroupTaxExempted
{
    public class EditClientGroupTaxExemptedEndpoint : EndpointBase<EditClientGroupTaxExemptedRequestViewModel, EditClientGroupTaxExemptedResponseViewModel>
    {
        public EditClientGroupTaxExemptedEndpoint(EndpointBaseParameters<EditClientGroupTaxExemptedRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditClientGroupTaxExempted})]
        public async Task<EndPointResponse<EditClientGroupTaxExemptedResponseViewModel>> Put(EditClientGroupTaxExemptedRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<EditClientGroupTaxExemptedCommand>());

            if (result.IsSuccess)
                return EndPointResponse<EditClientGroupTaxExemptedResponseViewModel>.Success(new EditClientGroupTaxExemptedResponseViewModel(), "TaxExempted Updated successfully.");
            else
                return EndPointResponse<EditClientGroupTaxExemptedResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
