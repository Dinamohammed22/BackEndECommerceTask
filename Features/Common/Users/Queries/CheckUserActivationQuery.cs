using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Users;
namespace KOG.ECommerce.Features.Common.Users.Queries
{
    public record CheckUserActivationQuery(string ID) : IRequestBase<bool>;
    public class CheckUserActivationQueryHandler : RequestHandlerBase<User, CheckUserActivationQuery, bool>
    {
        public CheckUserActivationQueryHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<bool>> Handle(CheckUserActivationQuery request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(c => c.ID == request.ID && c.IsActive == true);
            return RequestResult<bool>.Success(check);
        }
    }

}
