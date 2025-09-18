using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Companies.EditPointToCash.Commands
{
    public record EditPointToCashCommand(string Id, int NumberOfPoints, double AmountOfMoney) :IRequestBase<bool>;
    public class EditPointToCashCommandHandler : RequestHandlerBase<Company, EditPointToCashCommand, bool>
    {
        public EditPointToCashCommandHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(EditPointToCashCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.Id);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Company company = new Company { ID = request.Id };
            //company.NumberOfPoints = request.NumberOfPoints;
            //company.AmountOfMoney = request.AmountOfMoney;
            //_repository.SaveIncluded(company,
            //    nameof(company.NumberOfPoints), nameof(company.AmountOfMoney));

            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
