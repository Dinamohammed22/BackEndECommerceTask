using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.EditUser.Commands
{
    public record EditUserCommand(string Id,string Name, string Mobile, string? JobTitle) :IRequestBase<bool>;
    public class EditUserCommandHandler : RequestHandlerBase<User, EditUserCommand, bool>
    {
        public EditUserCommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.Id);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            var phoneValid = _repository.Any(c => c.Mobile == request.Mobile && c.ID != request.Id);
            if (!phoneValid)
            {
                User user = new User { ID = request.Id };
                user.Mobile = request.Mobile;
                user.Name = request.Name;
                user.JobTitle = request.JobTitle;
                _repository.SaveIncluded(user,
                    nameof(user.Name), nameof(user.Mobile),nameof(user.JobTitle));

                _repository.SaveChanges();
                return RequestResult<bool>.Success(true);
            }
            else
            {
                return await Task.FromResult(RequestResult<bool>.Failure(ErrorCode.ExistMobile));
            }

        }
    }
}
