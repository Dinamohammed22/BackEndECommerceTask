using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Brands.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Brands.GetAllBrand
{
    public class GetAllBrandEndpoint : EndpointBase<GetAllBrandRequestViewModel, GetAllBrandResponseViewModel>
    {
        public GetAllBrandEndpoint(EndpointBaseParameters<GetAllBrandRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllBrand })]
        public async Task<EndPointResponse<IEnumerable<GetAllBrandResponseViewModel>>> GetList([FromQuery] GetAllBrandRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<GetAllBrandQuery>());

            var response = result.Data.MapList<GetAllBrandResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<IEnumerable<GetAllBrandResponseViewModel>>.Success(response, "Get Brands successfully.");
            else
                return EndPointResponse<IEnumerable<GetAllBrandResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
