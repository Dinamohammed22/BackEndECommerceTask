using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Governorates;
using Microsoft.EntityFrameworkCore;
namespace KOG.ECommerce.Features.Common.Governorates.Queries
{
    public record GetGovernorateByNameQuery(string Name) : IRequestBase<IEnumerable<GovernorateProfileDTO>>;

    public class GetGovernorateByNameQueryHandler : RequestHandlerBase<Governorate, GetGovernorateByNameQuery, IEnumerable<GovernorateProfileDTO>>
    {
        public GetGovernorateByNameQueryHandler(RequestHandlerBaseParameters<Governorate> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<IEnumerable<GovernorateProfileDTO>>> Handle(GetGovernorateByNameQuery request, CancellationToken cancellationToken)
        {
            var governorate = _repository
                .Get(c => c.Name.Contains(request.Name))
                .Map<GovernorateProfileDTO>();
   
            return RequestResult<IEnumerable<GovernorateProfileDTO>>.Success(governorate);
        }
    }
}
