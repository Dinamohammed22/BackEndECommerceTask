using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Common.Companies.Queries;

public record CheckCompanyHasClassificationQuery(string ID) : IRequestBase<bool>;

public class CheckCompanyHasClassificationQueryHandler : RequestHandlerBase<Company, CheckCompanyHasClassificationQuery, bool>
{
    public CheckCompanyHasClassificationQueryHandler(RequestHandlerBaseParameters<Company> parameters) : base(parameters) { }

    public async override Task<RequestResult<bool>> Handle(CheckCompanyHasClassificationQuery request, CancellationToken cancellationToken)
    {
        var findClassificationId = await _repository.AnyAsync(c => c.ClassificationId == request.ID);
        return RequestResult<bool>.Success(findClassificationId);
    }
}
