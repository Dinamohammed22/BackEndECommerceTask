//using KOG.ECommerce.Common.Enums;
//using KOG.ECommerce.Common.Requests;
//using KOG.ECommerce.Features.Products.ActivateProducts.Commands;
//using KOG.ECommerce.Models.Clients;
//using KOG.ECommerce.Models.Products;
//using Microsoft.EntityFrameworkCore;

//namespace KOG.ECommerce.Features.Clients.ActivateClients.Commands
//{
//    public record ActivateClientsCommand(string ID) : IRequestBase<bool>;

//    public class ActivateClientsCommandHandler : RequestHandlerBase<Client, ActivateClientsCommand, bool>
//    {
//        public ActivateClientsCommandHandler(RequestHandlerBaseParameters<Client> parameters) : base(parameters)
//        {
//        }

//        public async override Task<RequestResult<bool>> Handle(ActivateClientsCommand request, CancellationToken cancellationToken)
//        {
//            var clientId = _repository.Any(c => c.ID == request.ID);
//            if (!clientId)
//                return RequestResult<bool>.Failure(ErrorCode.NotFound);
//            Client client = new Client { ID = request.ID };
//            client.IsActive = true;
//            _repository.SaveIncluded(client, nameof(client.IsActive));
//            _repository.SaveChanges();
//            var result = RequestResult<bool>.Success(true);
//            return await Task.FromResult(result);
//        }
//    }

//}
