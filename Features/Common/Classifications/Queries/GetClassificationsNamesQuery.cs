using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Features.Common.Classifications.DTOs;
using KOG.ECommerce.Models.Categories;
using KOG.ECommerce.Models.Classifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace KOG.ECommerce.Features.Common.Classifications.Queries
{
    public record GetClassificationsNamesQuery():IRequestBase<IEnumerable<GetClassificationsNamesDTO>>;
    public class GetClassificationsNamesQueryHandler : RequestHandlerBase<Classification, GetClassificationsNamesQuery, IEnumerable<GetClassificationsNamesDTO>>
    {
        public GetClassificationsNamesQueryHandler(RequestHandlerBaseParameters<Classification> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetClassificationsNamesDTO>>> Handle(GetClassificationsNamesQuery request, CancellationToken cancellationToken)
        {
            var classifications = await _repository.Get()
                .Include(c => c.Companies)
                .ToListAsync();

            var result = classifications
                .Where(c => GetActiveCompanyCount(c) > 0)
                .Select(c => new GetClassificationsNamesDTO(
                    c.ID,
                    c.Name,
                    GetActiveCompanyCount(c)
                ))
                .ToList();

            return RequestResult<IEnumerable<GetClassificationsNamesDTO>>.Success(result);
        }

        private static int GetActiveCompanyCount(Classification classification)
        {
            return classification.Companies?.Count(company => company.IsActive) ?? 0;
        }
    }
}
