//using KOG.ECommerce.Common.Enums;
//using KOG.ECommerce.Common.Requests;
//using KOG.ECommerce.Models.Clients;

//namespace KOG.ECommerce.Features.Clients.DeactivateClients.Commands
//{
//    public record DeactivateClientsCommand(string ID) : IRequestBase<bool>;

//    public class DeactivateClientsCommandHandler : RequestHandlerBase<Client, DeactivateClientsCommand, bool>
//    {
//        public DeactivateClientsCommandHandler(RequestHandlerBaseParameters<Client> parameters) : base(parameters)
//        {
//        }

//        public async override Task<RequestResult<bool>> Handle(DeactivateClientsCommand request, CancellationToken cancellationToken)
//        {
//            var clientId = _repository.Any(c => c.ID == request.ID);
//            if (!clientId)
//                return RequestResult<bool>.Failure(ErrorCode.NotFound);
//            Client client = new Client { ID = request.ID };

//            client.IsActive = false;
//            _repository.SaveIncluded(client, nameof(client.IsActive));
//            _repository.SaveChanges();
//            var result = RequestResult<bool>.Success(true);
//            return await Task.FromResult(result);
//        }
//    }

//}
