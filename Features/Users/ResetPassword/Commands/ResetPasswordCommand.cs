using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Users.Login.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Users;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;

namespace KOG.ECommerce.Features.Users.ResetPassword.Commands
{
    public record ResetPasswordCommand(string UserID, string Password, string ConfirmPassword) : IRequestBase<string>;
    public class ResetPasswordCommandHandler : RequestHandlerBase<User, ResetPasswordCommand, string>
    {
        public ResetPasswordCommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }
        
        public override async Task<RequestResult<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {

            if (request.Password != request.ConfirmPassword)
            {
                return RequestResult<string>.Failure();
            }

            //var userId = await _repository.Get(u => u.OTPtoken == request.Token && u.OTPExpiration > DateTime.UtcNow)
                                        //.Select(u=>u.ID).FirstOrDefaultAsync();

            var userId = request.UserID;

            if (string.IsNullOrEmpty(userId))
            {
                return RequestResult<string>.Failure();
            }
            var password = PasswordHasher.Hash(request.Password);
            var user = new User
            {
                ID = userId,
                Password = password
            };

            await _repository.SaveIncludedAsync(user, nameof(user.Password));
            _repository.SaveChanges();
            var token = await _mediator.Send(new LoginCommand(userId));

            return RequestResult<string>.Success(token.Data.Token);
        }

    }
}
