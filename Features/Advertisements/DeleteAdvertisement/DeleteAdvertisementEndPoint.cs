using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Advertisements.DeleteAdvertisement.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Advertisements.DeleteAdvertisement
{
    public class DeleteAdvertisementEndPoint : EndpointBase<DeleteAdvertisementRequestViewModel, DeleteAdvertisementResponseViewModel>
    {
        public DeleteAdvertisementEndPoint(EndpointBaseParameters<DeleteAdvertisementRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeleteAdvertisement })]
        public async Task<EndPointResponse<DeleteAdvertisementResponseViewModel>> DeleteAdvertisement(DeleteAdvertisementRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeleteAdvertisementCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeleteAdvertisementResponseViewModel>.Success(new DeleteAdvertisementResponseViewModel(), "Advertisement Deleted Successfully");
            else
                return EndPointResponse<DeleteAdvertisementResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
