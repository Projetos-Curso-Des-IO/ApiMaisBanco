using ApiFuncional.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace ApiFuncional.Configuration
{
	public static class AutenticacaoConfigBase
	{
		public static WebApplicationBuilder AddApiAutenticacao(this WebApplicationBuilder builder)
		{

			//Pega o Token e gera a chave encodado
			var JwtSettingSection = builder.Configuration.GetSection("JwtSettings");
			builder.Services.Configure<JwtSettings>(JwtSettingSection);


			var jwtSettings = JwtSettingSection.Get<JwtSettings>();
			var key = Encoding.ASCII.GetBytes(jwtSettings.Segredo);


			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = true;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidAudience = jwtSettings.Audiencia,
					ValidIssuer = jwtSettings.Emissor,
					RoleClaimType = ClaimTypes.Role
				};
			});


			return builder;
		}
	}
}