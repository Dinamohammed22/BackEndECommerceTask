using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;
using KOG.ECommerce.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Users.Queries
{
    public record UserDataQuery() : IRequestBase<UserDataDTO>;

    public class UserDataQueryHandler : RequestHandlerBase<User, UserDataQuery, UserDataDTO>
    {
        public UserDataQueryHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<UserDataDTO>> Handle(UserDataQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(u => u.ID == _userState.UserID)
                .Select(u => new UserDataDTO(
                    u.ID,
                    u.Name,
                    u.Mobile,
                    u.Client != null ? u.Client.Email : u.Company != null ? u.Company.Email : null,
                    null 
                ))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return RequestResult<UserDataDTO>.Failure(ErrorCode.Unauthorize);
            }

            var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(user.ID, SourceType.Client));

            return RequestResult<UserDataDTO>.Success(new UserDataDTO(
                user.ID,
                user.Name,
                user.Phone,
                user.Email,
                mediaResult.IsSuccess ? mediaResult.Data : string.Empty
            ));
        }

    }
}
