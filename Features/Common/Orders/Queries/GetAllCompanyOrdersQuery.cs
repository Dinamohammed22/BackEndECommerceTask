using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public class GetAllCompanyOrdersQuery : IRequestBase<PagingViewModel<OrdersDTO>>
    {
        public string? CustomerName { get; set; }
        public string? CustomerNumber { get; set; }
        public string? OrderNumber { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
    public class GetAllCompanyOrdersQueryHandler : RequestHandlerBase<Order, GetAllCompanyOrdersQuery, PagingViewModel<OrdersDTO>>
    {
        public GetAllCompanyOrdersQueryHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<OrdersDTO>>> Handle(GetAllCompanyOrdersQuery request, CancellationToken cancellationToken)
        {
            var companyId = _userState.UserID;

            var predicate = PredicateExtensions.PredicateExtensions.Begin<Order>(true);
            predicate = predicate
                .And(o => string.IsNullOrEmpty(request.CustomerName) || o.Client.Name.Contains(request.CustomerName))
                .And(o => string.IsNullOrEmpty(request.CustomerNumber) || o.Client.Mobile.Contains(request.CustomerNumber))
                .And(o => string.IsNullOrEmpty(request.OrderNumber) || o.OrderNumber.Contains(request.OrderNumber))
                .And(o => !request.OrderStatus.HasValue || o.Status == request.OrderStatus)
                .And(o => !request.TotalPrice.HasValue || o.TotalPrice == request.TotalPrice)
                .And(o => !request.From.HasValue || o.CreatedDate >= request.From.Value)
                .And(o => !request.To.HasValue || o.CreatedDate <= request.To.Value)
                .And(o => o.OrderItems.Any(oi => oi.Product.CompanyId == companyId));

            var query = _repository.Get(predicate)
                .Include(o => o.Client)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Select(order => new OrdersDTO
                {
                    ID = order.ID,
                    OrderNumber = order.OrderNumber,
                    Name = order.Client.Name,
                    Mobile = order.Client.Mobile,
                    OrderStatus = order.Status,
                    CreatedDate = order.CreatedDate,
                    ShippingAddressStatus = order.ShippingAddress.Status,
                    ShippingAddressId = order.ShippingAddressId,
                    TotalPrice = order.OrderItems
                .Where(oi => oi.Product.CompanyId == companyId)
                .Sum(oi => oi.Price),
                    TotalNetPrice = order.OrderItems
                .Where(oi => oi.Product.CompanyId == companyId)
                .Sum(oi => oi.NetPrice),
                    TotalLiter = order.OrderItems
                .Where(oi => oi.Product.CompanyId == companyId)
                .Sum(oi => oi.Liter)
                });

            var pageing = await query.ToPagesAsync(request.PageIndex, request.PageSize);

            return RequestResult<PagingViewModel<OrdersDTO>>.Success(pageing);
        }
    }
}
