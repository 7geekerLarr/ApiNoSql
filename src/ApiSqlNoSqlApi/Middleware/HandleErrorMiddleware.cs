using ApiNoSqlApplication.HandleError;
using Newtonsoft.Json;
using System.Net;

namespace ApiNoSqlApi.Middleware
{
    public class HandleErrorMiddleware
    {
        #region IDependencies        
        private readonly RequestDelegate _next;
        private readonly ILogger<HandleErrorMiddleware> _logger;
        #endregion
        #region HandlerErrorMiddleware        
        public HandleErrorMiddleware(RequestDelegate next, ILogger<HandleErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        #endregion
        #region Invocar       
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandlerErrorAsyn(context, ex, _logger);
            }

        }
        #endregion
        #region HandlerErrorAsyn       
        private async Task HandlerErrorAsyn(HttpContext context, Exception ex, ILogger<HandleErrorMiddleware> logger)
        {
            object errors = "";

            switch (ex)
            {
                case HandleException me:

                    errors = me.Errors;
                    context.Response.StatusCode = (int)me.Code;
                    logger.LogError(context.Response.StatusCode + " - " + errors.ToString(), "Manejador Error");
                    break;
                case Exception e:
                    logger.LogError(ex, "Error de Servidor");
                    logger.LogError(ex.InnerException, "Error de Servidor");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

            }
            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errors });
                await context.Response.WriteAsync(resultados);
            }
        }
        #endregion
    }
}
