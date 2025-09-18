using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Governorates.GetGovernorateByID
{
    public class GetGovernorateByIDEndPoint : EndpointBase<GetGovernorateByIDRequestViewModel, GetGovernorateByIDResponseViewModel>
    {
        public GetGovernorateByIDEndPoint(EndpointBaseParameters<GetGovernorateByIDRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetGovernoratebyID })]
        public async Task<EndPointResponse<GetGovernorateByIDResponseViewModel>> GetGovernorateByID([FromQuery] GetGovernorateByIDRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetGovernorateByIDQuery>());

            GetGovernorateByIDResponseViewModel response = result.Data.MapOne<GetGovernorateByIDResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<GetGovernorateByIDResponseViewModel>.Success(response, "Governorate filtered successfully");
            else
                return EndPointResponse<GetGovernorateByIDResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
