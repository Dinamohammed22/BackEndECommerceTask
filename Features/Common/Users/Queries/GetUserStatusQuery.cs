using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.Queries
{
    public record GetUserStatusQuery(string ID):IRequestBase<VerifyStatus>;
    public class GetUserStatusQueryHandler : RequestHandlerBase<User, GetUserStatusQuery, VerifyStatus>
    {
        public GetUserStatusQueryHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<VerifyStatus>> Handle(GetUserStatusQuery request, CancellationToken cancellationToken)
        {
            var status=_repository.GetByID(request.ID).VerifyStatus;
            if (status == null)
            {
                return RequestResult<VerifyStatus>.Failure(ErrorCode.NotFound);
            }
            return RequestResult<VerifyStatus>.Success(status); 
        }
    }
}
