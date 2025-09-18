using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.Clients.DeleteClient.Commands
{
    public record DeleteClientCommand(string ID) : IRequestBase<bool>;
    public class DleteClientCommandHandler : RequestHandlerBase<Client, DeleteClientCommand, bool>
    {
        public DleteClientCommandHandler(RequestHandlerBaseParameters<Client> parameters) : base(parameters) { }

        public async override Task<RequestResult<bool>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(p => p.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Client Client = new Client();
            Client.ID = request.ID;
            _repository.Delete(request.ID);
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
