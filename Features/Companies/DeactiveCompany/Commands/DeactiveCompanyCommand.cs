using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Companies.DeactiveCompany.Commands;

public record DeactiveCompanyCommand(string ID) : IRequestBase<bool>;
public class DeactiveCompanyCommandHandler : RequestHandlerBase<Company, DeactiveCompanyCommand, bool>
{
    public DeactiveCompanyCommandHandler(RequestHandlerBaseParameters<Company> parameters) : base(parameters) { }

    public async override Task<RequestResult<bool>> Handle(DeactiveCompanyCommand request, CancellationToken cancellationToken)
    {
        var check = await _repository.AnyAsync(b => b.ID == request.ID);
        if (!check)
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        Company company = new Company { ID = request.ID };
        company.IsActive = false;
        _repository.SaveIncluded(company, nameof(company.IsActive));
        _repository.SaveChanges();
        var result = RequestResult<bool>.Success(true);
        return await Task.FromResult(result);
    }
}

