using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Classifications.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Classifications.GetClassificationByID
{
    public class GetClassificationByIDEndpoint : EndpointBase<GetClassificationByIDRequestViewModel, GetClassificationByIDResponseViewModel>
    {
        public GetClassificationByIDEndpoint(EndpointBaseParameters<GetClassificationByIDRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetClassificationByID })]
        public async Task<EndPointResponse<GetClassificationByIDResponseViewModel>> GetClassificationByID([FromQuery] GetClassificationByIDRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetClassificationByIDQuery>());

            var response = result.Data.MapOne<GetClassificationByIDResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<GetClassificationByIDResponseViewModel>.Success(response, "Classification got successfully.");
            else
                return EndPointResponse<GetClassificationByIDResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
