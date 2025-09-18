using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Data;
using KOG.ECommerce.Models;
using MediatR;

namespace KOG.ECommerce.Common.Requests;

public interface IRequestBase<TResponse> : IRequest<RequestResult<TResponse>>
{ }
