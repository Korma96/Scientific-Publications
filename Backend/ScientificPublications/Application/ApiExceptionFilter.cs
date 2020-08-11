using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ScientificPublications.Common;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Extensions;
using System.Net;
using System.Text;

namespace ScientificPublications.Application
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException)
            {
                Log(context, LogLevel.Warning);
                CreateHttpResponse(context, (int)HttpStatusCode.BadRequest, context.Exception.Message);
            }
            else if (context.Exception is UnauthorizedException)
            {
                Log(context, LogLevel.Warning);
                CreateHttpResponse(context, (int)HttpStatusCode.Unauthorized, context.Exception.Message);
            }
            else if (context.Exception is ProxyException)
            {
                Log(context, LogLevel.Error);
                CreateHttpResponse(context, (int)HttpStatusCode.BadGateway, context.Exception.Message);
            }
            else
            {
                Log(context, LogLevel.Error);
                CreateHttpResponse(context, (int)HttpStatusCode.InternalServerError, context.Exception.Message);
            }
            context.ExceptionHandled = true;
        }

        private void CreateHttpResponse(ExceptionContext context, int statusCode, string message = null)
        {
            context.HttpContext.Response.StatusCode = statusCode;
            if (message != null)
            {
                context.HttpContext.Response.ContentType = Constants.XmlContentType;
                context.HttpContext.Response.Body.Write(Encoding.UTF8.GetBytes(message));
            }
        }

        private string GetRelativeUrl(ExceptionContext context) => context.HttpContext?.Request?.Path;

        private string GetRequest(ExceptionContext context) => context.HttpContext?.Request?.Body?.StreamToString();

        private void Log(ExceptionContext context, LogLevel logLevel)
        {
            var sb = new StringBuilder();
            string url = GetRelativeUrl(context);
            string request = GetRequest(context); // to do: investigate why request is always emtpy string ?

            if (!string.IsNullOrEmpty(url))
                sb.AppendLine($"\n\turl => {url}");

            if (!string.IsNullOrEmpty(request))
                sb.AppendLine($"\trequest => {request}");

            _logger.Log(logLevel, context.Exception, sb.ToString());
        }
    }
}
