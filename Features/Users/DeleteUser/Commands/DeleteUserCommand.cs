using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Users.DeleteUser.Commands
{
    public record DeleteUserCommand(string ID) : IRequestBase<bool>;
    public class DeleteUserCommandHandler : RequestHandlerBase<User, DeleteUserCommand, bool>
    {
        public DeleteUserCommandHandler(RequestHandlerBaseParameters<User> parameters) : base(parameters) { }

        public async override Task<RequestResult<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(p => p.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            User User = new User();
            User.ID = request.ID;
            _repository.Delete(request.ID);
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
