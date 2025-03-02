using ApiFuncional.Data;
using Microsoft.AspNetCore.Identity;

namespace ApiFuncional.Configuration
{
	public static class IdentityConfigBase
	{
		public static WebApplicationBuilder AddApiIdentity(this WebApplicationBuilder builder)
		{
			builder.Services.AddIdentity<IdentityUser, IdentityRole>()
							.AddRoles<IdentityRole>()
							.AddEntityFrameworkStores<ApiDbContext>();
			return builder;
		}
	}
}