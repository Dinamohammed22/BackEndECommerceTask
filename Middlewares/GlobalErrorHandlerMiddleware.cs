using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Exceptions;
using KOG.ECommerce.Helpers;
using Roboost.Common.Views;

namespace KOG.ECommerce.Middlewares;

public class GlobalErrorHandlerMiddleware
{
    readonly RequestDelegate _next;
    readonly ILogger<GlobalErrorHandlerMiddleware> _logger;
    public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            //var logFilePath = "C:\\Logs\\ECommerce\\ErrorLog.txt";
            //var logMessage = $"[{DateTime.Now}] Exception: {ex.Message}{Environment.NewLine}";

            //if (ex.InnerException != null)
            //{
            //    logMessage += $"Inner Exception: {ex.InnerException.Message}{Environment.NewLine}";
            //}

            //// Ensure directory exists
            //Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

            //// Append log to the file
            //File.AppendAllText(logFilePath, logMessage);


            ErrorCode errorCode = ErrorCode.UnKnown;
            string message = ErrorCode.UnKnown.GetDescription();

            if (ex is BusinessException businessException)
            {
                message = businessException.Message;
                errorCode = businessException.ErrorCode;
            }
            else if (ex is UnauthorizedAccessException unauthorized)
            {
                message = ErrorCode.Unauthorize.GetDescription();
                errorCode = ErrorCode.Unauthorize;
            }

            var result = EndPointResponse<bool>.Failure(errorCode, message);
            await context.Response.WriteAsJsonAsync(result);
        }
    }
}
