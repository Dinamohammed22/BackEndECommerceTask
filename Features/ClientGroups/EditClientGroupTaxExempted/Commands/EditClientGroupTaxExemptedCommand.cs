using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.ClientGroups;

namespace KOG.ECommerce.Features.ClientGroups.EditClientGroupTaxExempted.Commands
{
    public record EditClientGroupTaxExemptedCommand(string ID, bool TaxExempted):IRequestBase<bool>;
    public class EditClientGroupTaxExemptedCommandHandler : RequestHandlerBase<ClientGroup, EditClientGroupTaxExemptedCommand, bool>
    {
        public EditClientGroupTaxExemptedCommandHandler(RequestHandlerBaseParameters<ClientGroup> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(EditClientGroupTaxExemptedCommand request, CancellationToken cancellationToken)
        {
            var check=await _repository.AnyAsync(c=>c.ID==request.ID);
            if (!check)
            {
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            }
            ClientGroup clientGroup=new ClientGroup{ID=request.ID,TaxExempted=request.TaxExempted};
            _repository.SaveIncluded(clientGroup,nameof(clientGroup.TaxExempted));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
