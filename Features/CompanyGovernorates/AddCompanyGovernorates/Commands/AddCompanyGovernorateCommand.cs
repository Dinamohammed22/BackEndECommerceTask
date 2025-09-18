using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.CompanyGovernorates;

namespace KOG.ECommerce.Features.CompanyGovernorates.AddCompanyGovernorates.Commands
{
    public record AddCompanyGovernorateCommand(string CompanyId,List<string> GovernorateIds):IRequestBase<bool>;
    public class AddCompanyGovernorateCommandHandler : RequestHandlerBase<CompanyGovernorate, AddCompanyGovernorateCommand, bool>
    {
        public AddCompanyGovernorateCommandHandler(RequestHandlerBaseParameters<CompanyGovernorate> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddCompanyGovernorateCommand request, CancellationToken cancellationToken)
        {
            var distinctGovernorateIds = request.GovernorateIds.Distinct().ToList();
            var newEntries = distinctGovernorateIds.Select(govId => new CompanyGovernorate
            {
                CompanyId = request.CompanyId,
                GovernorateId = govId,
            });

             _repository.AddRange(newEntries);
            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);
        }
    }
}
