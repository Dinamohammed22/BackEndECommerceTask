using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Governorates;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Governorates.Queries
{
    public record GetListGovernorateQuery(int pageIndex = 1, int pageSize = 100) : IRequestBase<PagingViewModel<GovernorateProfileDTO>>;
    
        public class GetListGovernorateQueryHandler : RequestHandlerBase<Governorate, GetListGovernorateQuery, PagingViewModel<GovernorateProfileDTO>>
        {
            public GetListGovernorateQueryHandler(RequestHandlerBaseParameters<Governorate> parameters) : base(parameters)
            {
            }

            public override async Task<RequestResult<PagingViewModel<GovernorateProfileDTO>>> Handle(GetListGovernorateQuery request, CancellationToken cancellationToken)
            {

                var governorate = await _repository
                .Get()
                .Map<GovernorateProfileDTO>()
                .ToPagesAsync(request.pageIndex, request.pageSize);

            return RequestResult<PagingViewModel<GovernorateProfileDTO>>.Success(governorate);
            }
        }
    
}
