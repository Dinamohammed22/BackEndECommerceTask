using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Companies.CompanyRegister.Orchestrators;
using KOG.ECommerce.Features.Companies.CreateCompany.Orchestrators;
using KOG.ECommerce.Helpers;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.CompanyRegister
{
    public class CompanyRegisterEndPoint : EndpointBase<CompanyRegisterRequestViewModel, CompanyRegisterResponseViewModel>
    {
        public CompanyRegisterEndPoint(EndpointBaseParameters<CompanyRegisterRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CompanyRegister })]
        public async Task<EndPointResponse<CompanyRegisterResponseViewModel>> CompanyRegister(CompanyRegisterRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<CompanyRegisterOrchestrator>());

            if (result.IsSuccess)
                return EndPointResponse<CompanyRegisterResponseViewModel>.Success(result.Data.MapOne<CompanyRegisterResponseViewModel>(), "Company Registered successfully");
            else
                return EndPointResponse<CompanyRegisterResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
