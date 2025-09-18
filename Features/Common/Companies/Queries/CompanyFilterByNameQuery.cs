using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Companies;
using MediatR.Wrappers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Companies.Queries
{
    public record CompanyFilterByNameQuery(
        string? NID,
        DateTime? From,
        DateTime? To,
        int pageIndex = 1,
        int pageSize = 100,
        string? CityID = null,
        string? GovernorateID = null,
        string? CompanyName = null
    ) : IRequestBase<PagingViewModel<GetAllCompaniesDTO>>;

    public class CompanyFilterByNameQueryHandler : RequestHandlerBase<Company, CompanyFilterByNameQuery, PagingViewModel<GetAllCompaniesDTO>>
    {
        public CompanyFilterByNameQueryHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<GetAllCompaniesDTO>>> Handle(CompanyFilterByNameQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<Company>(true);

            predicate = predicate
                .And(c => string.IsNullOrEmpty(request.CompanyName) || c.Name.Contains(request.CompanyName))
                .And(c => string.IsNullOrEmpty(request.NID) || c.NID.Contains(request.NID))
                .And(c => string.IsNullOrEmpty(request.CityID) || c.CityId == request.CityID)
                .And(c => string.IsNullOrEmpty(request.GovernorateID) || c.GovernorateId == request.GovernorateID)
                .And(o => !request.From.HasValue || o.CreatedDate >= request.From.Value)
                .And(o => !request.To.HasValue || o.CreatedDate <= request.To.Value);

            var query = await _repository.Get(predicate)
                .Include(c => c.City)
                .Include(c => c.Governorate)
                .Include(c => c.Classification)
                .Map<GetAllCompaniesDTO>()
                .ToPagesAsync(request.pageIndex, request.pageSize);

            foreach (var item in query.Items)
            {
                var CompanyTotalSales = await _mediator.Send(new GetCompanyTotalSalesByIdQuery(item.ID));
                if (CompanyTotalSales.Data is not null)
                {
                    item.TotalPrice = CompanyTotalSales.Data.TotalPrice ?? 0;
                    item.TotalNetPrice = CompanyTotalSales.Data.TotalNetPrice ?? 0;
                    item.TotalLiter = CompanyTotalSales.Data.TotalWeight ?? 0;
                }
            }
            return RequestResult<PagingViewModel<GetAllCompaniesDTO>>.Success(query);
        }

    }
}
