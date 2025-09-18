using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Features.Common.WishlistProducts.Queries;
using KOG.ECommerce.Features.NotificationMessages.SendNotification.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.WishlistProducts;

namespace KOG.ECommerce.Features.WishlistProducts.NotifyClientsOfRestockedWishlistProducts.Orchestrator
{
    public record NotifyClientsOfRestockedWishlistProductsOrchestrator(string ProductID):IRequestBase<bool>;
    public class NotifyClientsOfRestockedWishlistProductsOrchestratorHandler : RequestHandlerBase<WishlistProduct, NotifyClientsOfRestockedWishlistProductsOrchestrator, bool>
    {
        public NotifyClientsOfRestockedWishlistProductsOrchestratorHandler(RequestHandlerBaseParameters<WishlistProduct> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<RequestResult<bool>> Handle(NotifyClientsOfRestockedWishlistProductsOrchestrator request, CancellationToken cancellationToken)
        {
            var ClientIDS = await _mediator.Send(new GetClientsIfProductInWishListQuery(request.ProductID));
            var ProductName = (await _mediator.Send(new GetProductNameAndPriceQuery(request.ProductID))).Data.Name;
            //var message = $"Dear Client, The product {ProductName} is Restocked ,You can order it now";

            foreach (var ClientID in ClientIDS.Data)
            {
                //var ClientPhoneNumber = await _mediator.Send(new GetClientMobileQuery(ClientID));
                //var smsResponse = await SMSHelper.SendSmsAsync(ClientPhoneNumber.Data, message);
                //if (!smsResponse.Success)
                //{
                //    return RequestResult<bool>.Failure(ErrorCode.ProductRestockdAndCannotSend);
                //}
                var ToFirebaseToken = await _mediator.Send(new GetFirebaseTokenByUserIDQuery(ClientID));
                if (!ToFirebaseToken.IsSuccess)
                {
                    return RequestResult<bool>.Failure(ToFirebaseToken.ErrorCode);
                }
                var ToSendNotification = await _mediator.Send(new SendNotificationCommand(
                       ClientID,$"المنتج {ProductName} متوفر الآن",$"المنتج الذي قمت بإضافته إلى قائمة الأمنيات ({ProductName}) أصبح متوفراً الآن! سارع بطلبه قبل نفاد الكمية.",
                         ToFirebaseToken.Data));
                if (!ToSendNotification.IsSuccess)
                {
                    return RequestResult<bool>.Failure(ToSendNotification.ErrorCode);
                }
            }
            return RequestResult<bool>.Success();
        }
    }
}
