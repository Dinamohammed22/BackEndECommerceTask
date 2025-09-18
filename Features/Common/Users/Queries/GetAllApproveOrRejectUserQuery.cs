using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Users.Queries
{
    public record GetAllApproveOrRejectUserQuery(int PageIndex=1,int PageSize=100) :IRequestBase<PagingViewModel<VerifiedStatusDTO>>;
    public class GetAllApproveOrRejectUserQueryHandler : RequestHandlerBase<User, GetAllApproveOrRejectUserQuery, PagingViewModel<VerifiedStatusDTO>>
    {
        public GetAllApproveOrRejectUserQueryHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<VerifiedStatusDTO>>> Handle(GetAllApproveOrRejectUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository
                .Get(u =>(u.RoleId==Role.Client) &&(u.VerifyStatus == VerifyStatus.Reject || u.VerifyStatus == VerifyStatus.Approve))
                .Include(u => u.Client)
                    .ThenInclude(c => c.ShippingAddresses)
                        .ThenInclude(sa => sa.Governorate)
                .Include(u => u.Client)
                    .ThenInclude(c => c.ShippingAddresses)
                        .ThenInclude(sa => sa.City)
                .Map<VerifiedStatusDTO>()
                .ToPagesAsync(request.PageIndex, request.PageSize);

            return RequestResult<PagingViewModel<VerifiedStatusDTO>>.Success(users);
        }
    }
}
