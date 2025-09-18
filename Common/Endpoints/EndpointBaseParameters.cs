using FluentValidation;
using KOG.ECommerce.Data;
using MediatR;

namespace KOG.ECommerce.Common.Endpoints;

public class EndpointBaseParameters<TRequest>
{
    private readonly IMediator _mediator;
    private readonly IValidator<TRequest> _validator;

    public IMediator Mediator => _mediator;
    public IValidator<TRequest> Validator => _validator;
    private UserState _userState;

    private Entities _context;

    public EndpointBaseParameters(Entities context, IMediator mediator, IValidator<TRequest> validator, UserState userState)
    {
        _mediator = mediator;
        _validator = validator;
        _context = context;
        _userState = userState;
    }
}
