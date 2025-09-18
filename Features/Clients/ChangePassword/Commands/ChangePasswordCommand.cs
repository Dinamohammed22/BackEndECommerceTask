using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.Clients.ChangePassword.Commands
{
    public record ChangePasswordCommand(string Password,string? ID):IRequestBase<bool>;
    public class ChangePasswordCommandHandler : RequestHandlerBase<Client, ChangePasswordCommand, bool>
    {
        public ChangePasswordCommandHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Client client = new Client { ID = request.ID };
            client.Password = request.Password;
            _repository.SaveIncluded(client, nameof(client.Password));
            _repository.SaveChanges();
            return await Task.FromResult(RequestResult<bool>.Success(true));
        }
    }
}
