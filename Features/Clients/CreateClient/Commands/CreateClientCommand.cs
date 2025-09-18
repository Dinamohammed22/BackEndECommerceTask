using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.CreateClient.Commands
{
    public record CreateClientCommand(string? NationalNumber,string Name,
        string Mobile,  string Password, string ConfirmPassword, string ID, string? Email,
        string? ClientGroupId, string? Phone, ClientActivity? ClientActivity, Religion Religion) : IRequestBase<string>;
    public class CreateClientCommandHandler : RequestHandlerBase<Client, CreateClientCommand, string>
    {
        public CreateClientCommandHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {

            var password = PasswordHasher.Hash(request.Password);
            Client client = new Client
                    {
                        ID = request.ID,
                        Name = request.Name,
                        Mobile = request.Mobile,
                        Password = password,
                        NationalNumber = request.NationalNumber,
                        Email = request.Email,
                        Phone= request.Phone,
                        ClientGroupId = request.ClientGroupId,
                        ClientActivity=request.ClientActivity,
                        Religion=request.Religion
                    };
                    _repository.Add(client);
                    _repository.SaveChanges();
            return RequestResult<string>.Success(client.ID);

        }
    }

}
