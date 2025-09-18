using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.CompanyGovernorates;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.CompanyGovernorates.BulkDeleteCompanyGovernorates.Command
{
    public record BulkDeleteCompanyGovernoratesCommand(string CompanyId) : IRequestBase<bool>;
    public class BulkDeleteCompanyGovernoratesCommandHandler : RequestHandlerBase<CompanyGovernorate, BulkDeleteCompanyGovernoratesCommand, bool>
    {
        public BulkDeleteCompanyGovernoratesCommandHandler(RequestHandlerBaseParameters<CompanyGovernorate> requestParameters) : base(requestParameters) { }

        public async override Task<RequestResult<bool>> Handle(BulkDeleteCompanyGovernoratesCommand request, CancellationToken cancellationToken)
        {
            var CompanyGovernorates = await _repository.Get(c => c.CompanyId == request.CompanyId).ToListAsync();

            if (CompanyGovernorates == null)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            _repository.BulkSoftDelete(CompanyGovernorates);

            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
