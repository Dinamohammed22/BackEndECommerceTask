using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.ClientGroups;

namespace KOG.ECommerce.Features.ClientGroups.CreateClientGroup.Commands
{
    public record CreateClientGroupCommand(string Name, bool TaxExempted):IRequestBase<bool>;
    public class ClientGroupCommandHandler : RequestHandlerBase<ClientGroup, CreateClientGroupCommand, bool>
    {
        public ClientGroupCommandHandler(RequestHandlerBaseParameters<ClientGroup> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CreateClientGroupCommand request, CancellationToken cancellationToken)
        {
            ClientGroup clientGroup = new ClientGroup { Name = request.Name, TaxExempted = request.TaxExempted };
            _repository.Add(clientGroup);
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }

}
