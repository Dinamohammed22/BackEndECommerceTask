using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Common.Governorates.Queries
{
    public record CheckGovernorateActivationQuery(string GovernorateId):IRequestBase<bool>;
    public class CheckGovernorateActivationQueryHandler : RequestHandlerBase<Governorate, CheckGovernorateActivationQuery, bool>
    {
        public CheckGovernorateActivationQueryHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckGovernorateActivationQuery request, CancellationToken cancellationToken)
        {
            var IsActiveGovernorate = await _repository.AnyAsync(c => c.ID == request.GovernorateId && c.IsActive);
            return RequestResult<bool>.Success(IsActiveGovernorate);
        }
    }


}
