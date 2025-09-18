using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Clients.ClientRegister.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Clients.RecoverAccount.Commands
{
    public record RecoverAccountCommand(string Mobile, string Password, string ConfirmPassword):IRequestBase<bool>;
    public class RecoverAccountCommandHandler : RequestHandlerBase<Client, RecoverAccountCommand, bool>
    {
        public RecoverAccountCommandHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(RecoverAccountCommand request, CancellationToken cancellationToken)
        {

            var client = _repository.Get(c => c.Mobile == request.Mobile).FirstOrDefault();
            if (client!=null)
            {
                var password = PasswordHasher.Hash(request.Password);
                client.Password=password;
             
                _repository.SaveIncluded(client, nameof(client.Password));
                _repository.SaveChanges();
                return RequestResult<bool>.Success(true);
            }
            else
            {
                
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            }
        
        }
  }

}
