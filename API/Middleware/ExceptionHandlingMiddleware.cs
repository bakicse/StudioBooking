using Shared.Common;
using System.Diagnostics;

namespace Application.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        if (!context.Response.HasStarted)
        {
            var frame = new StackTrace(e, true).GetFrames().FirstOrDefault(f => f.GetFileLineNumber() > 0);
            var logMessage = $"{nameof(Exception)}: {e.Message}\nInner Exception: {e.InnerException?.Message ?? "No Inner Exception"}\nTrace: {frame}";
            logger.LogError(logMessage);

            context.Response.ContentType = "application/json";
            var response = new ExceptionResponseModel
            {
                StatusCode = e.GetType().Name,
                ExceptionMessage = e.Message
            };
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
    }

    #region HttpStatusCode
    //private static int GetStatusCodeForException(Exception ex)
    //{
    //    return ex is HttpRequestException e ? e.StatusCode switch
    //    {
    //        HttpStatusCode.BadRequest => StatusCodes.Status400BadRequest,
    //        HttpStatusCode.Unauthorized => StatusCodes.Status401Unauthorized,
    //        HttpStatusCode.PaymentRequired => StatusCodes.Status402PaymentRequired,
    //        HttpStatusCode.Forbidden => StatusCodes.Status403Forbidden,
    //        HttpStatusCode.NotFound => StatusCodes.Status404NotFound,
    //        HttpStatusCode.MethodNotAllowed => StatusCodes.Status405MethodNotAllowed,
    //        HttpStatusCode.NotAcceptable => StatusCodes.Status406NotAcceptable,
    //        HttpStatusCode.RequestTimeout => StatusCodes.Status408RequestTimeout,
    //        HttpStatusCode.Conflict => StatusCodes.Status409Conflict,
    //        HttpStatusCode.RequestEntityTooLarge => StatusCodes.Status413RequestEntityTooLarge,
    //        HttpStatusCode.RequestUriTooLong => StatusCodes.Status414RequestUriTooLong,
    //        HttpStatusCode.UnsupportedMediaType => StatusCodes.Status415UnsupportedMediaType,
    //        _ => StatusCodes.Status500InternalServerError
    //    } : StatusCodes.Status500InternalServerError;
    //}
    #endregion
}
