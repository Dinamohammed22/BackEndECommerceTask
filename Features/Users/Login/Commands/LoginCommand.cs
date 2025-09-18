using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Users.Login.Commands
{
    public record LoginCommand(string ID):IRequestBase<LoginDTO>;
    public class LoginCommandHandler : RequestHandlerBase<User, LoginCommand, LoginDTO>
    {
        public LoginCommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<LoginDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user=_repository.Get(c=>c.ID == request.ID).FirstOrDefault();
            if (user != null) {
                var token= TokenGenerator.Generate(user.ID, user.Mobile, user.RoleId);
                var result = new LoginDTO(Token:token, RoleId:user.RoleId);

                return RequestResult<LoginDTO>.Success(result);
            }
            return RequestResult<LoginDTO>.Failure(ErrorCode.ValidationErrors);
        }
    }
}
