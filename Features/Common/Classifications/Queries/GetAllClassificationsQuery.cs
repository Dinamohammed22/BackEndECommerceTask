using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Classifications.DTOs;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Classifications;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Classifications.Queries
{
    public record GetAllClassificationsQuery(string? Name = null, int pageIndex = 1, int pageSize = 100) : IRequestBase<PagingViewModel<GetAllClassificationsDTO>>;
    public class GetAllClassificationsQueryHandler : RequestHandlerBase<Classification, GetAllClassificationsQuery, PagingViewModel<GetAllClassificationsDTO>>
    {
        public GetAllClassificationsQueryHandler(RequestHandlerBaseParameters<Classification> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<GetAllClassificationsDTO>>> Handle(GetAllClassificationsQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<Classification>(true);

            predicate = predicate
            .And(c => string.IsNullOrEmpty(request.Name) || c.Name.Contains(request.Name));
            var classifications = await _repository
                .Get(predicate)
                .Select(g => new GetAllClassificationsDTO
                {
                    ID = g.ID,
                    Name = g.Name,
                    Companies = g.Companies.Where(c => !c.Deleted).Select(c => new CompanyForClassificationDTO
                    {
                        Name = c.Name,
                    }).ToList(),
                })
                .ToPagesAsync(request.pageIndex, request.pageSize);

            return RequestResult<PagingViewModel<GetAllClassificationsDTO>>.Success(classifications);
        }
    }
}
