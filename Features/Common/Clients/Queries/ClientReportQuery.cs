using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public record ClientReportQuery(string? Name, DateTime? From, DateTime? To, int pageIndex = 1, int pageSize = 100):IRequestBase<PagingViewModel<ClientReportDTO>>;
    public class ClientReportQueryHandler : RequestHandlerBase<Client, ClientReportQuery, PagingViewModel<ClientReportDTO>>
    {
        public ClientReportQueryHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<ClientReportDTO>>> Handle(ClientReportQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<Client>(true);
            predicate = predicate
                .And(c => string.IsNullOrEmpty(request.Name) || c.Name.Contains(request.Name))
                .And(c => !request.From.HasValue || c.CreatedDate >= request.From)
                .And(c => !request.To.HasValue || c.CreatedDate <= request.To);

            var query = _repository.GetWithDeleted()
          .Where(predicate)
          .Include(c => c.Orders)
          .Select(c => new ClientReportDTO
          {
              Name = c.Name,
              ClientActivity = c.ClientActivity,
              Mobile = c.Mobile,
              TotalPrice = c.Orders.Where(o => o.Status == OrderStatus.Completed).Sum(o => o.TotalPrice),
              TotalLiter = c.Orders.Where(o => o.Status == OrderStatus.Completed).Sum(o => o.TotalLiter)
          })
          .OrderByDescending(dto => dto.TotalPrice);


            var pagedResult = await query.ToPagesAsync(request.pageIndex, request.pageSize);

            return RequestResult<PagingViewModel<ClientReportDTO>>.Success(pagedResult);
        }

    }
}
