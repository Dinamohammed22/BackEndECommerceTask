using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.ChangeUserPassword.Commands
{
    public record ChangeUserPasswordCommand(string Password, string ConfirmPassword, string? ID) : IRequestBase<bool>;
    public class ChangeUserPasswordCommandHandler : RequestHandlerBase<User, ChangeUserPasswordCommand, bool>
    {
        public ChangeUserPasswordCommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var ID = request.ID;
            if (ID == null)
                ID = _userState.UserID;
            var check = await _repository.AnyAsync(b => b.ID == ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            var password = PasswordHasher.Hash(request.Password);
            User user = new User { ID = ID };
            user.Password = password;
            _repository.SaveIncluded(user, nameof(user.Password));
            _repository.SaveChanges();
            return await Task.FromResult(RequestResult<bool>.Success(true));
        }
    }
}
