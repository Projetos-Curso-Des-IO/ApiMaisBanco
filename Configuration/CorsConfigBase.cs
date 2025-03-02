namespace ApiFuncional.Configuration
{
	public static class CorsConfigBase
	{
		public static WebApplicationBuilder AddApiCors(this WebApplicationBuilder builder)
		{
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("Development", builder =>
					builder
						.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());

				options.AddPolicy("Production", builder =>
					builder
						.WithOrigins("https://localhost:9000")
						.WithMethods("POST")
						.AllowAnyHeader());
			});
			return builder;
		}
	}
}