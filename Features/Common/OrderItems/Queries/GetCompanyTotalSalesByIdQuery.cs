using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Models.OrderItems;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Companies.Queries
{
    public record GetCompanyTotalSalesByIdQuery(string CompanyId) : IRequestBase<CompanyTotalStaticsDTO>;

    public class GetCompanyTotalSalesByIdQueryHandler : RequestHandlerBase<OrderItem, GetCompanyTotalSalesByIdQuery, CompanyTotalStaticsDTO>
    {
        public GetCompanyTotalSalesByIdQueryHandler(RequestHandlerBaseParameters<OrderItem> requestParameters)
            : base(requestParameters)
        {
        }

        public override async Task<RequestResult<CompanyTotalStaticsDTO>> Handle(GetCompanyTotalSalesByIdQuery request, CancellationToken cancellationToken)
        {
            var sales = await _repository.Get()
                .Where(oi => oi.Product.CompanyId == request.CompanyId)
                .Select(oi => new
                {
                    oi.Price,
                    oi.NetPrice,
                    oi.Liter
                })
                .ToListAsync(cancellationToken);

            if (!sales.Any())
            {
                return RequestResult<CompanyTotalStaticsDTO>.Failure(ErrorCode.NotFound);
            }

            var dto = new CompanyTotalStaticsDTO
            {
                TotalPrice = Math.Round(sales.Sum(s => (double)s.Price), 2),
                TotalNetPrice = Math.Round(sales.Sum(s => (double)s.NetPrice), 2),
                TotalWeight = Math.Round(sales.Sum(s => (double)s.Liter), 2)
            };

            return RequestResult<CompanyTotalStaticsDTO>.Success(dto);
        }
    }
}
