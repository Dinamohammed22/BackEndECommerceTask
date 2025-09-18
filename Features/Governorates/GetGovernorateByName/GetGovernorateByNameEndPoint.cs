using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Governorates.GetGovernorateByName
{
    public class GetGovernorateByNameEndPoint : EndpointBase<GetGovernorateByNameRequestViewModel, GetGovernorateByNameResponseViewModel>
    {
        public GetGovernorateByNameEndPoint(EndpointBaseParameters<GetGovernorateByNameRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetGovernorateByName })]
        public async Task<EndPointResponse<IEnumerable<GetGovernorateByNameResponseViewModel>>> GetByName([FromQuery] GetGovernorateByNameRequestViewModel viewModel)
        {
           
            var result = await _mediator.Send(viewModel.MapOne<GetGovernorateByNameQuery>());

            IEnumerable<GetGovernorateByNameResponseViewModel> response = result.Data.MapList<GetGovernorateByNameResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse< IEnumerable<GetGovernorateByNameResponseViewModel>>.Success(response, "Governorate filtered successfully");
            else
                return EndPointResponse< IEnumerable<GetGovernorateByNameResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
