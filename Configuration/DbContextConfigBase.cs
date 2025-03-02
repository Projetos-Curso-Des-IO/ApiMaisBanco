using ApiFuncional.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiFuncional.Configuration
{
	public static class DbContextConfigBase
	{
		public static WebApplicationBuilder AddApiDbContext(this WebApplicationBuilder builder)
		{
			builder.Services.AddDbContext<ApiDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
			);
			return builder;
		}
	}
}