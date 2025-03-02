
namespace ApiFuncional.Configuration
{
	public static class ApiConfig
	{
		public static WebApplicationBuilder AddApiController(this WebApplicationBuilder builder)
		{
			builder.Services.AddControllers()
							.ConfigureApiBehaviorOptions(optioins =>
							{
								optioins.SuppressModelStateInvalidFilter = true;
							});
			return builder;
		}	
	}
}
