using System.Net;
using System.Text.Json;

namespace Order_Management.Errors
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMiddleware(RequestDelegate Next,ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
			_next = Next;
			this._logger = logger;
			this._env = env;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next.Invoke(context); // in case no exceptionhappened
			}
			catch (Exception ex) 
			{ 
				_logger.LogError(ex,ex.Message);
				//prouction = log ex in database
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				//var Response = _env.IsDevelopment()? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError,
				//	ex.Message, ex.StackTrace.ToString())
				//	: new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
				var Response = _env.IsDevelopment()
				? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
				: new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
				var Options = new JsonSerializerOptions()
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase //for front end side it needs camelcase
				}; 
				var jsonResponse = JsonSerializer.Serialize(Response,Options);
				await context.Response.WriteAsync(jsonResponse);
			}
		}
    }
}
