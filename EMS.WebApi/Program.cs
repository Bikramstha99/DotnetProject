using CEHRD.IEMIS.WebAPI.Seeds;
using EMS.Business.Interface;
using EMS.Business.Service;
using EMS.Entities.Dtos;
using EMS.Repository;
using EMS.WebApi.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureScopedServices();

builder.Services.ConfigureIdentityServices(builder.Configuration);
builder.Services.ConfigureSqlDependencies(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{

    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "EMS API",
        Description = "API listing of the EMS System",
    });
});

builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.AddMemoryCache();


builder.Services.AddCors(options =>
{
options.AddPolicy("CorsPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
});

var app = builder.Build();
await SeedData.InitializeDefaultData(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
