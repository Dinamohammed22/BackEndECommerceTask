using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Clients.EditClient.Commands;
using KOG.ECommerce.Features.Common.Users.EditUser.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public record CheckClientMobileAndNationalNumberQuery(string NationalNumber,string Mobile,string ID) :IRequestBase<bool>;
    public class CheckClientMobileAndNationalNumberQueryHandler : RequestHandlerBase<Client, CheckClientMobileAndNationalNumberQuery, bool>
    {
        public CheckClientMobileAndNationalNumberQueryHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckClientMobileAndNationalNumberQuery request, CancellationToken cancellationToken)
        {
            var phoneValid = _repository.Any(c => c.Mobile == request.Mobile && c.ID != request.ID);
            if (!phoneValid)
            {
                var NationalNumberValid = await _repository.AnyAsync(c => c.NationalNumber == request.NationalNumber && c.ID != request.ID);
                if (!NationalNumberValid)
                {
                            return RequestResult<bool>.Success(true);
                }
                else
                {
                    return RequestResult<bool>.Failure(ErrorCode.ExistNationalNumber);
                }
            }
            else
            {
                return await Task.FromResult(RequestResult<bool>.Failure(ErrorCode.ExistMobile));
            }
        }
    }
}
