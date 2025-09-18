using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Users.Queries
{
    public record GetAllUsersQuery(string? Mobile, string? UserName,int pageIndex = 1,
        int pageSize = 100):IRequestBase<PagingViewModel<GetAllUsersDTO>>;
    public class GetAllUsersQueryHandler : RequestHandlerBase<User, GetAllUsersQuery, PagingViewModel<GetAllUsersDTO>>
    {
        public GetAllUsersQueryHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<GetAllUsersDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<User>(true);

            predicate = predicate
                .And(c => string.IsNullOrEmpty(request.Mobile) || c.Mobile.Contains(request.Mobile))
                .And(c => c.RoleId != Role.Client); 
            ;
            var query = await _repository.Get(predicate).Map<GetAllUsersDTO>().ToPagesAsync(request.pageIndex, request.pageSize);

            return RequestResult<PagingViewModel<GetAllUsersDTO>>.Success(query);

        }
    }
}
