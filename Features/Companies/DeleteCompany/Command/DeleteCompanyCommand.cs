using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Companies.DeleteCompany.Command
{
    public record DeleteCompanyCommand(string Id) : IRequestBase<bool>;
    public class DeleteCompanyCommandHandler : RequestHandlerBase< Company, DeleteCompanyCommand, bool>
    {
        public DeleteCompanyCommandHandler(RequestHandlerBaseParameters<Company> parameters)
            : base(parameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.Id);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            _repository.Delete(request.Id);
            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);
        }
    }
}
