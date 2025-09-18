using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Clients.RecoverAccount.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.RecoverAccount
{
    public class RecoverAccountEndPoint : EndpointBase<RecoverAccountRequestViewModel, RecoverAccountResponseViewModel>
    {
        public RecoverAccountEndPoint(EndpointBaseParameters<RecoverAccountRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.RecoverAccount })]
        public async Task<EndPointResponse<RecoverAccountResponseViewModel>> RecoverAccount(RecoverAccountRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<RecoverAccountCommand>());
            if (result.IsSuccess)
            {
                return EndPointResponse<RecoverAccountResponseViewModel>.Success(new RecoverAccountResponseViewModel(), "Account Recovered successfully.");
            }
            return EndPointResponse<RecoverAccountResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
