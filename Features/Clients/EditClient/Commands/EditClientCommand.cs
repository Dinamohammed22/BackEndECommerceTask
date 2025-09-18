using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.EditClient.Commands
{
    public record EditClientCommand(string ID, string? NationalNumber, string Name,
        string Mobile, string? Email, string? ClientGroupId, string? Phone, ClientActivity? ClientActivity) : IRequestBase<bool>;
    public class EditClientCommandHandler : RequestHandlerBase<Client, EditClientCommand, bool>
    {
        public EditClientCommandHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(EditClientCommand request, CancellationToken cancellationToken)
        {
            var client=_repository.GetByID(request.ID);
            if (client == null) {
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            }
            client.Name = request.Name;
            client.Mobile = request.Mobile;
            client.Email = request.Email;
            client.NationalNumber = request.NationalNumber;
            client.ClientGroupId = request.ClientGroupId;
            client.Phone = request.Phone; 
            client.ClientActivity = request.ClientActivity;
            _repository.SaveIncluded(client,nameof(client.Name),nameof(client.Mobile),
                nameof(client.Email),nameof(client.NationalNumber),nameof(client.ClientGroupId)
                ,nameof(client.Phone),nameof(client.ClientActivity));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
