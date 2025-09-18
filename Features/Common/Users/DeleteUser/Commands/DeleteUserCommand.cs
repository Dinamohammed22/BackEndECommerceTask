using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.DeleteUser.Commands
{
    public record DeleteUserCommand(string Id) : IRequestBase<bool>;
    public class DeleteUserCommandHandler : RequestHandlerBase<User, DeleteUserCommand, bool>
    {
        public DeleteUserCommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.Id);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            _repository.Delete(request.Id);
            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);
        }
    }
}
