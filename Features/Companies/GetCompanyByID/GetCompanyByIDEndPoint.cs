using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Companies;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace Roboost.Features.Companys
{
    public class GetCompanyByIDEndPoint : EndpointBase<GetCompanyByIDRequestViewModel, GetCompanyByIDResponseViewModel>
    {
        public GetCompanyByIDEndPoint(EndpointBaseParameters<GetCompanyByIDRequestViewModel> parameters):base(parameters) { }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetCompanyById })]
        public async Task<EndPointResponse<GetCompanyByIDResponseViewModel>> GetByID([FromQuery]GetCompanyByIDRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<GetCompanyByIDQuery>());

            var response = result.Data.MapOne<GetCompanyByIDResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<GetCompanyByIDResponseViewModel>.Success(response, "Companies filtered successfully");
            else
                return EndPointResponse<GetCompanyByIDResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
