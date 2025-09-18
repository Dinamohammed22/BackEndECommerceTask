using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.CreateUser.Commands
{
    public record CreateUserCommand(string Name, string Password, string ConfirmPassword, string Mobile, Role RoleId,
         string? JobTitle, VerifyStatus VerifyStatus= VerifyStatus.Approve) : IRequestBase<string>;
    public class CreateUserCommandHandler : RequestHandlerBase<User, CreateUserCommand, string>
    {
        public CreateUserCommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var phoneValid = _repository.Get(c => c.Mobile == request.Mobile).FirstOrDefault();
            if (phoneValid == null)
            {
                var password = PasswordHasher.Hash(request.Password);
                User user = new User { Name = request.Name, Password =password, 
                    RoleId = request.RoleId, Mobile = request.Mobile ,VerifyStatus=request.VerifyStatus,
                JobTitle=request.JobTitle,IsActive=true};
                _repository.Add(user);
                _repository.SaveChanges();
                var UserId = user.ID;
                var result = RequestResult<string>.Success(UserId);

                return await Task.FromResult(result);
            }
            return RequestResult<string>.Failure(ErrorCode.ExistMobile);
        }
    }
}
