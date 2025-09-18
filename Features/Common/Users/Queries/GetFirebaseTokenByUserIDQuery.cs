using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.Queries
{
    public record GetFirebaseTokenByUserIDQuery(string ID):IRequestBase<string>;
    public class GetFirebaseTokenByUserIDQueryHandler : RequestHandlerBase<User, GetFirebaseTokenByUserIDQuery, string>
    {
        public GetFirebaseTokenByUserIDQueryHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(GetFirebaseTokenByUserIDQuery request, CancellationToken cancellationToken)
        {
            var user = _repository.GetByID(request.ID);

            if (user == null)
                return RequestResult<string>.Failure(ErrorCode.NotFound);

            if (string.IsNullOrEmpty(user.FirebaseToken))
            {
                var errorMessage = $"Client '{user.Name}' is not logged in.";
                return RequestResult<string>.Failure(ErrorCode.ClientNotLoggedInBefore, errorMessage);
            }

            return RequestResult<string>.Success(user.FirebaseToken);
        }
    }
}
