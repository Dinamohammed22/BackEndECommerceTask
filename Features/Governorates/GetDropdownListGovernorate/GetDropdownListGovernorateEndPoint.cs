using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Governorates.GetDropdownListGovernorate
{
    public class GetDropdownListGovernorateEndPoint : EndpointBase<GetDropdownListGovernorateRequestViewModel, GetDropdownListGovernorateResponseViewModel>
    {
        public GetDropdownListGovernorateEndPoint(EndpointBaseParameters<GetDropdownListGovernorateRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetGovernorateDropdownList })]
        public async Task<EndPointResponse<IEnumerable<GetDropdownListGovernorateResponseViewModel>>> GetDropdownList()
        {


            var result = await _mediator.Send(new GetDropdownListGovernorateQuery());

            var response = result.Data.MapList<GetDropdownListGovernorateResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<IEnumerable<GetDropdownListGovernorateResponseViewModel>>.Success(response, "Governorate filtered successfully");
            else
                return EndPointResponse<IEnumerable<GetDropdownListGovernorateResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
