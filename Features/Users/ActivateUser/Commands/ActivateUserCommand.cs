using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Users.ActivateUser.Commands
{
    public record ActivateUserCommand(string ID) : IRequestBase<bool>;
    public class ActivateUserCommandHandler : RequestHandlerBase<User, ActivateUserCommand, bool>
    {
        public ActivateUserCommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var UserId = _repository.Any(c => c.ID == request.ID);
            if (!UserId)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            User user = new User { ID = request.ID };
            user.IsActive = true;
            _repository.SaveIncluded(user, nameof(user.IsActive));
            _repository.SaveChanges();
            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);
        }
    }
}
