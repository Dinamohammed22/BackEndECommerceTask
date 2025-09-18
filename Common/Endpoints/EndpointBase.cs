using FluentValidation;
using KOG.ECommerce.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Common.Endpoints;

[ApiController]
[Route("[controller]/[action]")]
//[ServiceFilter(typeof(AuthorizeFilter))]
public abstract class EndpointBase<TRequest, TResponse> : ControllerBase
{
    protected readonly IMediator _mediator;
    protected readonly IValidator<TRequest> _validator;

    public EndpointBase(EndpointBaseParameters<TRequest> dependencyCollection)
    {
        _mediator = dependencyCollection.Mediator;
        _validator = dependencyCollection.Validator;
    }

    protected virtual async Task<EndPointResponse<TResponse>> ValidateRequestAsync(TRequest request)
    {
        var validationResults = await _validator.ValidateAsync(request);

        if (validationResults.IsValid)
            return EndPointResponse<TResponse>.Success(default, "Validation successful");


        var validationErrors = string.Join(", ", validationResults.Errors.Select(e => e.ErrorMessage));
        var errMsg = string.Format("Validation failed:\n {0}", validationErrors);

        return EndPointResponse<TResponse>.Failure(ErrorCode.ValidationErrors, errMsg);

    }
}
