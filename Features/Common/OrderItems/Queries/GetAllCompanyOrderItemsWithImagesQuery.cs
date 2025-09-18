using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.OrderItems;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.OrderItems.Queries
{
    public record GetAllCompanyOrderItemsWithImagesQuery(string OrderId) : IRequestBase<IEnumerable<OrderItemsDTO>>;
    public class GetAllCompanyOrderItemsWithImagesQueryHandler : RequestHandlerBase<OrderItem, GetAllCompanyOrderItemsWithImagesQuery, IEnumerable<OrderItemsDTO>>
    {
        public GetAllCompanyOrderItemsWithImagesQueryHandler(RequestHandlerBaseParameters<OrderItem> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<OrderItemsDTO>>> Handle(GetAllCompanyOrderItemsWithImagesQuery request, CancellationToken cancellationToken)
        {
            var companyId = _userState.UserID;

            var orderItems = await _repository.Get(x => x.OrderId == request.OrderId && x.Product.CompanyId == companyId)
                .Include(x => x.Product)
                .Select(x => new OrderItemsDTO(
                    x.ProductId,
                    x.Quantity,
                    x.ItemPrice,
                    x.Price,
                    x.NetPrice,
                    x.Product.Name,
                    x.ItemLiter,
                    x.Liter,
                    x.ItemPoint,
                    x.Point,
                    null,
                    x.Product.Company.Name,
                    x.Product.CompanyId
                ))
                .ToListAsync(cancellationToken);

            if (!orderItems.Any())
            {
                return RequestResult<IEnumerable<OrderItemsDTO>>.Failure(ErrorCode.NotFound);
            }

            var orderItemsDTOs = new List<OrderItemsDTO>();
            foreach (var item in orderItems)
            {
                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(item.ProductId, SourceType.Product), cancellationToken);
                var orderItemDTO = item with
                {
                    Path = mediaResult.IsSuccess ? mediaResult.Data : null
                };
                orderItemsDTOs.Add(orderItemDTO);
            }

            return RequestResult<IEnumerable<OrderItemsDTO>>.Success(orderItemsDTOs);
        }
    }
}
