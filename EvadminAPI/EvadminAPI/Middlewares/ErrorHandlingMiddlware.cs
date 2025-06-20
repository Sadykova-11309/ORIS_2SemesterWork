namespace EvadminAPI.Middlewares
{
	public class ErrorHandlingMiddlware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ErrorHandlingMiddlware> _logger;

		public ErrorHandlingMiddlware(RequestDelegate next, ILogger<ErrorHandlingMiddlware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Path.StartsWithSegments("/Error/404") || context.Request.Path.StartsWithSegments("/Error/500"))
			{
				await _next(context);
				return;
			}
			try
			{
				await _next(context);

				if (context.Response.StatusCode == StatusCodes.Status404NotFound)
				{
					context.Response.Redirect("/Error/404", permanent: false);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Произошла ошибка при обработке запроса");

				context.Response.Redirect("/Error/500", permanent: false);
			}
		}
	}
}
