using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.ClientGroups.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.ClientGroups;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.ClientGroups.Queries
{
    public record ClientGroupFilterByNameQuery( string? Name, int pageIndex = 1,
        int pageSize = 100) :IRequestBase<PagingViewModel<ClientGroupProfileDTO>>;
    public class ClientGroupFilterByNameQueryHandler : RequestHandlerBase<ClientGroup, ClientGroupFilterByNameQuery, PagingViewModel<ClientGroupProfileDTO>>
    {
        public ClientGroupFilterByNameQueryHandler(RequestHandlerBaseParameters<ClientGroup> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<ClientGroupProfileDTO>>> Handle(ClientGroupFilterByNameQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<ClientGroup>(true);

            predicate = predicate
                .And(c => string.IsNullOrEmpty(request.Name) || c.Name.Contains(request.Name));



            var query = await _repository.Get(predicate).Map<ClientGroupProfileDTO>().ToPagesAsync(request.pageIndex, request.pageSize); ;

            return RequestResult<PagingViewModel<ClientGroupProfileDTO>>.Success(query);
        }
    }
}
