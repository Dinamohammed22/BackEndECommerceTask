using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Users.ResetPassword.Commands;
using KOG.ECommerce.Features.Users.ResetPasswordOTP;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Users.ResetPassword
{
    public class ResetPasswordEndpoint : EndpointBase<ResetPasswordRequest, ResetPasswordResponse>
    {
        public ResetPasswordEndpoint(EndpointBaseParameters<ResetPasswordRequest> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPut]
        //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ResetPassword })]
        public async Task<EndPointResponse<ResetPasswordResponse>> ResetPassword (ResetPasswordRequest request)
        {
            var result = await _mediator.Send(request.MapOne<ResetPasswordCommand>());
            if (result.IsSuccess)
            {
                return EndPointResponse<ResetPasswordResponse>.Success(new ResetPasswordResponse(result.Data), "password reset successfully");
            }
            return EndPointResponse<ResetPasswordResponse>.Failure(result.ErrorCode);

        }
    }
}
