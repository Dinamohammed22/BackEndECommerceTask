using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.Queries
{
    public record GetClientMobileQuery(string ID) : IRequestBase<string>;
    public class GetClientMobileQueryHandler : RequestHandlerBase<User, GetClientMobileQuery, string>
    {
        public GetClientMobileQueryHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(GetClientMobileQuery request, CancellationToken cancellationToken)
        {
            var mobile = _repository.Get(c => c.ID == request.ID && c.IsActive == true).FirstOrDefault().Mobile;

            return RequestResult<string>.Success(mobile);
        }
    }
}
