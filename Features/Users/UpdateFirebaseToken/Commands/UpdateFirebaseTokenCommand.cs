using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Users.UpdateFirebaseToken.Commands
{
    public record UpdateFirebaseTokenCommand(string ID, string FirebaseToken):IRequestBase<bool>;
    public class UpdateFirebaseTokenCommandHandler : RequestHandlerBase<User, UpdateFirebaseTokenCommand, bool>
    {
        public UpdateFirebaseTokenCommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(UpdateFirebaseTokenCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            User user = new User { ID = request.ID };
            user.FirebaseToken = request.FirebaseToken;
            _repository.SaveIncluded(user, nameof(user.FirebaseToken));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
