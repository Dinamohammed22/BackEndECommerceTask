using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Companies.Queries
{
    public record GetAllPendingCompanyQuery(int PageIndex = 1, int PageSize = 100) : IRequestBase<PagingViewModel<CompanyVerifyStatusDTO>>;
    public class GetAllPendingCompanyQueryHandler : RequestHandlerBase<Company, GetAllPendingCompanyQuery, PagingViewModel<CompanyVerifyStatusDTO>>
    {
        public GetAllPendingCompanyQueryHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<CompanyVerifyStatusDTO>>> Handle(GetAllPendingCompanyQuery request, CancellationToken cancellationToken)
        {
            var companies = await _repository
                           .Get()
                           .Where(c =>
                               c.User != null &&
                               c.User.RoleId == Role.Company &&
                               (c.User.VerifyStatus == VerifyStatus.Pending || c.User.VerifyStatus == VerifyStatus.Verified)
                           )
                           .Include(c => c.User)
                           .Include(c => c.Classification)
                           .Include(c => c.Governorate)
                           .Include(c => c.City)
                           .Map<CompanyVerifyStatusDTO>()
                           .ToPagesAsync(request.PageIndex, request.PageSize);

            return RequestResult<PagingViewModel<CompanyVerifyStatusDTO>>.Success(companies);
        }
    }
}
