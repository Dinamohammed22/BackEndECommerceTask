using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.GetCompanyRequestByID
{
    public class GetCompanyRequestByIDEndpoint : EndpointBase<GetCompanyRequestByIDRequestViewModel, GetCompanyRequestByIDResponseViewModel>
    {
        public GetCompanyRequestByIDEndpoint(EndpointBaseParameters<GetCompanyRequestByIDRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetCompanyRequestById })]
        public async Task<EndPointResponse<GetCompanyRequestByIDResponseViewModel>> GetCompanyRequestById([FromQuery] GetCompanyRequestByIDRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<GetCompanyRequestByIDQuery>());

            var response = result.Data.MapOne<GetCompanyRequestByIDResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<GetCompanyRequestByIDResponseViewModel>.Success(response, "Company request retrived successfully");
            else
                return EndPointResponse<GetCompanyRequestByIDResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
