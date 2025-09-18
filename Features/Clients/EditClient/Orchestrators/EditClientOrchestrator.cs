using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Clients.EditClient.Commands;
using KOG.ECommerce.Features.Common.Users.EditUser.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Clients.EditClient.Orchestrators
{
    public record EditClientOrchestrator(string ID, string? NationalNumber, string Name,
        string Mobile,string? Email, string? ClientGroupId, string? Phone,
        ClientActivity? ClientActivity) : IRequestBase<bool>;
    public class EditClientOrchestratorHandler : RequestHandlerBase<Client, EditClientOrchestrator, bool>
    {
        public EditClientOrchestratorHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(EditClientOrchestrator request, CancellationToken cancellationToken)
        {
            var phoneValid = _repository.Any(c => c.Mobile == request.Mobile && c.ID != request.ID);
            if (!phoneValid)
            {
                var NationalNumberValid = await _repository.AnyAsync(c => c.NationalNumber == request.NationalNumber && c.ID != request.ID);
                if (!NationalNumberValid || request.NationalNumber.IsNullOrEmpty())
                {
                    var user = await _mediator.Send(new EditUserCommand(request.ID,request.Name,request.Mobile,null));
                    if (user.IsSuccess)
                    {
                        var client = await _mediator.Send(request.MapOne<EditClientCommand>());
                        if (client.IsSuccess)
                        {
                            return RequestResult<bool>.Success(true);
                        }
                        else
                        {
                            return RequestResult<bool>.Failure(client.ErrorCode);
                        }
                    }
                    return RequestResult<bool>.Failure(user.ErrorCode);
                }
                else
                {
                    return RequestResult<bool>.Failure(ErrorCode.ExistNationalNumber);
                }
            }
            else
            {
                return await Task.FromResult(RequestResult<bool>.Failure(ErrorCode.ExistMobile));
            }
        }
    }
}
