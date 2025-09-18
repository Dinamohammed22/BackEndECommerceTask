using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Helpers;


namespace KOG.ECommerce.Features.Clients.ClientRegister.Commands
{
    public record ClientRegisterCommand( string ID , string? NationalNumber,string Name,
        string Mobile, string Password, string ConfirmPassword, 
        string? Email, string? Phone, ClientActivity? ClientActivity, Religion Religion) : IRequestBase<string>;


  
    public class ClientRegisterCommandHandler : RequestHandlerBase<Client, ClientRegisterCommand, string>
    {
        
        public ClientRegisterCommandHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(ClientRegisterCommand request, CancellationToken cancellationToken)
        {
            var password = PasswordHasher.Hash(request.Password);
            Client client = new Client
                    {
                        ID = request.ID,
                        Name = request.Name,
                        Mobile = request.Mobile,
                        Phone = request.Phone,
                        Password = password,
                        Email = request.Email,
                        NationalNumber = request.NationalNumber,
                        ClientActivity= request.ClientActivity,
                        Religion= request.Religion
                    };

                    _repository.Add(client);
                    _repository.SaveChanges();

                    return RequestResult<string>.Success(client.ID);
                
               
        }
       
    }
}
