using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Companies.ActiveCompany.Commands;

public record ActiveCompanyCommand(string ID) : IRequestBase<bool>;
public class ActiveCompanyCommandHandler : RequestHandlerBase<Company, ActiveCompanyCommand, bool>
{
    public ActiveCompanyCommandHandler(RequestHandlerBaseParameters<Company> parameters) : base(parameters) { }

    public async override Task<RequestResult<bool>> Handle(ActiveCompanyCommand request, CancellationToken cancellationToken)
    {
        var check = await _repository.AnyAsync(b => b.ID == request.ID);
        if (!check)
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        Company company=new Company { ID = request.ID };
        company.IsActive = true;
        _repository.SaveIncluded(company, nameof(company.IsActive));
        _repository.SaveChanges();
        var result = RequestResult<bool>.Success(true);
        return await Task.FromResult(result);
    }
}

