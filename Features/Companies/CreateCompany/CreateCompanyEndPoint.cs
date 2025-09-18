using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Companies.CreateCompany.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies;
public class CreateCompanyEndPoint : EndpointBase<CreateCompanyRequestViewModel, CreateCompanyResponseViewModel>
{
    public CreateCompanyEndPoint(EndpointBaseParameters<CreateCompanyRequestViewModel> dependencyParameters) : base(dependencyParameters)
    {

    }

    [HttpPost]
    [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreateCompany })]
    public async Task<EndPointResponse<CreateCompanyResponseViewModel>> Post(CreateCompanyRequestViewModel viewModel)
    {
        var validationResult = await ValidateRequestAsync(viewModel);

        if (!validationResult.IsSuccess)
            return validationResult;

        var result = await _mediator.Send(viewModel.MapOne<CreateCompanyOrchestrator>());

        if (result.IsSuccess)
            return EndPointResponse<CreateCompanyResponseViewModel>.Success(new CreateCompanyResponseViewModel(), "Company Added successfully");
        else
            return EndPointResponse<CreateCompanyResponseViewModel>.Failure(result.ErrorCode);

    }

}