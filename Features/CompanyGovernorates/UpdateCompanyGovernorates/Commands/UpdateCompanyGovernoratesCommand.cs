using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.CompanyGovernorates.AddCompanyGovernorates.Commands;
using KOG.ECommerce.Features.CompanyGovernorates.BulkDeleteCompanyGovernorates.Command;
using KOG.ECommerce.Models.CompanyGovernorates;

namespace KOG.ECommerce.Features.Companies.Commands
{
    public record UpdateCompanyGovernoratesCommand(string CompanyId, List<string> GovernorateIds) : IRequestBase<bool>;

    public class UpdateCompanyGovernoratesCommandHandler: RequestHandlerBase<CompanyGovernorate, UpdateCompanyGovernoratesCommand, bool>
    {
        public UpdateCompanyGovernoratesCommandHandler(RequestHandlerBaseParameters<CompanyGovernorate> requestParameters): base(requestParameters) { }

        public async override Task<RequestResult<bool>> Handle(UpdateCompanyGovernoratesCommand request, CancellationToken cancellationToken)
        {
            //remove old ones
            await _mediator.Send(new BulkDeleteCompanyGovernoratesCommand(request.CompanyId)); 

            //Add new ones
            await _mediator.Send(new AddCompanyGovernorateCommand(request.CompanyId, request.GovernorateIds));   

            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
