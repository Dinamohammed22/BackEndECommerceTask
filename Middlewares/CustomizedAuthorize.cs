using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.BlackListTokens.AddBlackListToken.Commands;
using KOG.ECommerce.Features.Common.RoleFeatures;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace KOG.ECommerce.Middlewares;

public class CustomizedAuthorizeAttribute : ActionFilterAttribute
{
    private readonly Feature _feature;
    private readonly UserState _userState;

    public CustomizedAuthorizeAttribute(Feature feature, UserState userState)
    {
        _feature = feature;
        _userState = userState;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var authHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
        var token = authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) ? authHeader[7..] : authHeader;

        var loggedUser = context.HttpContext.User;
        var userId = loggedUser.FindFirst("ID")?.Value ?? string.Empty;

        var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();

        if (!string.IsNullOrEmpty(userId))
        {
            var isActive = await mediator.Send(new CheckUserActivationQuery(userId));
            if (!isActive.Data)
            {
                await mediator.Send(new AddBlackListTokenCommand(token));
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsJsonAsync(new
                {
                    errorCode = ErrorCode.UnauthorizeTokenIsBlackListed.ToString(),
                    message = "User is deactivated."
                });
                return;
            }
        }

        var roleClaim = loggedUser.FindFirst("RoleID")?.Value;
        if (string.IsNullOrEmpty(roleClaim) || !Enum.TryParse(roleClaim, out Role roleId))
        {
            throw new UnauthorizedAccessException();
        }

        var hasAccess = await mediator.Send(new CheckRoleFeatureAccessQuery(roleId, _feature));
        if (!hasAccess.Data)
        {
            throw new UnauthorizedAccessException();
        }

        _userState.RoleID = roleId;
        _userState.UserID = userId;
        _userState.Username = loggedUser.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

        await next(); // Proceed with the request pipeline
    }
}
