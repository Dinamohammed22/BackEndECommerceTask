using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Governorates;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Governorates.Queries
{
    public record GetGovernorateByIDQuery(string ID) : IRequestBase<GetGovernorateByIDProfileDTO>;

    public class GetGovernorateByIDQueryHandler : RequestHandlerBase<Governorate, GetGovernorateByIDQuery, GetGovernorateByIDProfileDTO>
    {
        public GetGovernorateByIDQueryHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters)
        {
        }

        public async override  Task<RequestResult<GetGovernorateByIDProfileDTO>> Handle(GetGovernorateByIDQuery request, CancellationToken cancellationToken)
        {
            var governorate =await _repository
                .Get(g => g.ID == request.ID)
                .Map<GetGovernorateByIDProfileDTO>()
                .FirstOrDefaultAsync();

            return RequestResult<GetGovernorateByIDProfileDTO>.Success(governorate);
        }

        }


}
