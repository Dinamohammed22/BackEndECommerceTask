using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Clients.ClientRegister.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Clients.ClientRegister
{
    public class ClientRegisterEndPoint : EndpointBase<ClientRegisterRequestViewModel, ClientRegisterResponseViewModel>
    {
        public ClientRegisterEndPoint(EndpointBaseParameters<ClientRegisterRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ClientRegister })]
        public async Task<EndPointResponse<ClientRegisterResponseViewModel>> Post(ClientRegisterRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ClientRegisterOrchestrator>());
            if (result.IsSuccess)
            {
                return EndPointResponse<ClientRegisterResponseViewModel>.Success(result.Data.MapOne<ClientRegisterResponseViewModel>(), result.Message);
            }
            return EndPointResponse<ClientRegisterResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
