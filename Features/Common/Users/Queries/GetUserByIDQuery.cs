using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.Queries
{
    
    public record GetUserByIDQuery(string ID) : IRequestBase<GetUserByIDDTO>;
    public class GetUserByIDQueryHandler : RequestHandlerBase<User, GetUserByIDQuery, GetUserByIDDTO>
    {
        public GetUserByIDQueryHandler(RequestHandlerBaseParameters<User> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<GetUserByIDDTO>> Handle(GetUserByIDQuery request, CancellationToken cancellationToken)
        {

            var user = _repository.GetByID(request.ID).MapOne<GetUserByIDDTO>();

            if (user == null)
                return RequestResult<GetUserByIDDTO>.Failure();

            return RequestResult<GetUserByIDDTO>.Success(user);
        }
    }
}
