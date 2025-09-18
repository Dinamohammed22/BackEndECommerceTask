using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.ClientGroups;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.ClientGroups.EditClientGroup.Commands
{
    public record EditClientGroupCommand(string Id, string Name, bool TaxExempted) : IRequestBase<bool>;
    public class EditClientGroupCommandHandler : RequestHandlerBase<ClientGroup, EditClientGroupCommand, bool>
    {
        public EditClientGroupCommandHandler(RequestHandlerBaseParameters<ClientGroup> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(EditClientGroupCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.Id);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            ClientGroup client =new ClientGroup { ID = request.Id };
            client.Name=request.Name;
            client.TaxExempted=request.TaxExempted;
            _repository.SaveIncluded(client, nameof(client.Name), nameof(client.TaxExempted));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
