using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public record SearchClientQuery(
        string? Name,
        string? Email,
        string? Mobile ,
        string? NationalNumber,
        string? ClientGroupId,
        VerifyStatus? VerifyStatus,
        DateTime? From, 
        DateTime? To,
        bool? Deleted,
        Religion? Religion,
        int pageIndex = 1,
        int pageSize = 100
    ) : IRequestBase<PagingViewModel<SearchClientProfileDTO>>;

    public class GetSearchedClientsQueryHandler : RequestHandlerBase<Client, SearchClientQuery, PagingViewModel<SearchClientProfileDTO>>
    {
        public GetSearchedClientsQueryHandler(RequestHandlerBaseParameters<Client> parameters) : base(parameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<SearchClientProfileDTO>>> Handle(SearchClientQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<Client>(true);

            predicate = predicate
                .And(c => string.IsNullOrEmpty(request.Name) ? true : c.Name.Contains(request.Name))
                .And(c => string.IsNullOrEmpty(request.NationalNumber) ? true : c.NationalNumber.Contains(request.NationalNumber))
                .And(c => string.IsNullOrEmpty(request.Mobile) ? true : c.Mobile.Contains(request.Mobile)|| c.Phone.Contains(request.Mobile))
                .And(c => string.IsNullOrEmpty(request.Email) ? true : c.Email.Contains(request.Email))
                .And(c => string.IsNullOrEmpty(request.ClientGroupId) ? true : c.ClientGroupId.Contains(request.ClientGroupId))
                .And(c => !request.VerifyStatus.HasValue || c.User.VerifyStatus == request.VerifyStatus)
                .And(c => c.User.VerifyStatus != VerifyStatus.Reject)
                .And(c => !request.From.HasValue || c.CreatedDate >= request.From)
                .And(c => !request.To.HasValue || c.CreatedDate <= request.To)
                .And(c => !request.Deleted.HasValue || c.Deleted == request.Deleted).And(c => !request.To.HasValue || c.CreatedDate <= request.To)
                .And(c => !request.Religion.HasValue || c.Religion == request.Religion);

            var query = await _repository.GetWithDeleted().Where(predicate)
                .Include(c => c.ClientGroup)
                .Include(c => c.User)
                .Include(c => c.Orders)
                .OrderByDescending(c => c.CreatedDate)
                .Map<SearchClientProfileDTO>()
                .ToPagesAsync(request.pageIndex, request.pageSize);

            foreach (var dto in query.Items)
            {
                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(dto.ID, SourceType.Client));
                dto.Path = mediaResult.IsSuccess ? mediaResult.Data : string.Empty;
            }

            return RequestResult<PagingViewModel<SearchClientProfileDTO>>.Success(query);
        }
    }
}
