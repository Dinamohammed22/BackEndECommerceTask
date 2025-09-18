using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Data;
using KOG.ECommerce.Models;
using MediatR;

namespace KOG.ECommerce.Common.Requests;

public record RequestHandlerBaseParameters<T>(IMediator Mediator, UserState UserState, Repository<T> Repository) 
    where T : BaseModel, new();
