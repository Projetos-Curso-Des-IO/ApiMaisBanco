using ApiFuncional.Configuration;
using ApiFuncional.Data;
using ApiFuncional.Errors;
using ApiFuncional.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.AddApiController()
		.AddApiCors()
		.AddApiDbContext()
		.AddApiIdentity()
		.AddApiAutenticacao();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ApiFuncional.Service.ClientesService>();
builder.Services.AddScoped<ErrorsRespons>();

builder.AddApiSwagger();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseCors("Development");
}
else
{
	app.UseCors("Production");
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
