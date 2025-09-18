using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Users.Queries
{
    public record GetAllVerifiedStatusQuery(int pageIndex = 1, int pageSize = 100) :IRequestBase<PagingViewModel<VerifiedStatusDTO>>;
    public class GetAllVerifiedStatusQueryHandler : RequestHandlerBase<User, GetAllVerifiedStatusQuery, PagingViewModel<VerifiedStatusDTO>>
    {
        public GetAllVerifiedStatusQueryHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<VerifiedStatusDTO>>> Handle(GetAllVerifiedStatusQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository
                .Get(u => (u.RoleId == Role.Client) && (u.VerifyStatus == VerifyStatus.Verified|| u.VerifyStatus == VerifyStatus.Pending))
                .Include(u => u.Client)
                .ThenInclude(c => c.ShippingAddresses)
                .ThenInclude(sa => sa.Governorate)
                .Include(u => u.Client)
                .ThenInclude(c => c.ShippingAddresses)
                .ThenInclude(sa => sa.City)
                .Map<VerifiedStatusDTO>()
                .ToPagesAsync(request.pageIndex, request.pageSize);

            return RequestResult<PagingViewModel<VerifiedStatusDTO>>.Success(users);
        }
    }
}
