using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Cities.DeleteCity.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Cities.DeleteCity
{
    public class DeleteCityEndPoint : EndpointBase<DeleteCityRequestViewModel, DeleteCityResponseViewModel>
    {
        public DeleteCityEndPoint(EndpointBaseParameters<DeleteCityRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeleteCity })]
        public async Task<EndPointResponse<DeleteCityResponseViewModel>> Delete(DeleteCityRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeleteCityCommand>());
            if (result.IsSuccess)
            {
                return EndPointResponse<DeleteCityResponseViewModel>.Success(new DeleteCityResponseViewModel(), "City Deleted successfully.");
            }
            return EndPointResponse<DeleteCityResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
