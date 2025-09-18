using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.ChangeState.Commands
{
    public record ChangeStateCommand(string ID,VerifyStatus VerifyStatus):IRequestBase<bool>;
    public class ChangeStateCommandHandler : RequestHandlerBase<User, ChangeStateCommand, bool>
    {
        public ChangeStateCommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ChangeStateCommand request, CancellationToken cancellationToken)
        {
            var check=await _repository.AnyAsync(u=>u.ID==request.ID);
            if (check)
            {
                User user = new User { ID = request.ID };
                user.VerifyStatus = request.VerifyStatus;
                _repository.SaveIncluded(user, nameof(user.VerifyStatus));
                _repository.SaveChanges();
                return RequestResult<bool>.Success(true);
            }
            else
            {
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            }
        }
    }
}
