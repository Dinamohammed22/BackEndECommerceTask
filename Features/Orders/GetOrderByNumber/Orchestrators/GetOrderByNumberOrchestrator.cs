using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Features.Common.OrderItems.Queries;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Orders.GetOrderByNumber.Orchestrators
{
    public record GetOrderByNumberOrchestrator(string OrderNumber) : IRequestBase<GetOrderByNumberDTO>;
    public class GetOrderByNumberOrchestratorHandler : RequestHandlerBase<Order, GetOrderByNumberOrchestrator, GetOrderByNumberDTO>
    {
        public GetOrderByNumberOrchestratorHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetOrderByNumberDTO>> Handle(GetOrderByNumberOrchestrator request, CancellationToken cancellationToken)
        {
            var order = await _repository.Get(o => o.OrderNumber == request.OrderNumber)
               .Include(o => o.Client) 
               .Include(o => o.OrderItems) 
               .FirstOrDefaultAsync();

            if (order == null)
            {
                return RequestResult<GetOrderByNumberDTO>.Failure(ErrorCode.OrderNotFound);
            }
            var role=_userState.RoleID;
            RequestResult<IEnumerable<OrderItemsDTO>> orderItemsResult;
            if (role == Role.Company)
            {
                 orderItemsResult = await _mediator.Send(new GetAllCompanyOrderItemsWithImagesQuery(order.ID));
                if (!orderItemsResult.IsSuccess)
                {
                    return RequestResult<GetOrderByNumberDTO>.Failure(orderItemsResult.ErrorCode);
                }
            }
            else
            {
                 orderItemsResult = await _mediator.Send(new GetAllOrderItemsWithImagesQuery(order.ID));
                if (!orderItemsResult.IsSuccess)
                {
                    return RequestResult<GetOrderByNumberDTO>.Failure(orderItemsResult.ErrorCode);
                }
            }

            //calculate total Liter
            double TotalLiter = 0;

            foreach (var product in orderItemsResult.Data)
            {
                TotalLiter += product.ItemLiter * product.Quantity;
            }

            double totalPrice = orderItemsResult.Data.Sum(x => x.Price);
            double totalNetPrice = orderItemsResult.Data.Sum(x => x.NetPrice);

            var orderDTO = new GetOrderByNumberDTO(
                OrderNumber: order.OrderNumber,
                TotalPrice: totalPrice,
                TotalNetPrice: totalNetPrice,
                Status: order.Status,
                CreatedDate: order.CreatedDate,
                OrderId: order.ID,
                CustomerName: order.Client?.Name,
                Mobile: order.Client?.Mobile,
                Email: order.Client?.Email,
                OrderItems: orderItemsResult.Data.ToList(),
                TotalLiter: TotalLiter
            );


            return RequestResult<GetOrderByNumberDTO>.Success(orderDTO);
        }
    }
}
