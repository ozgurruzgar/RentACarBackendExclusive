using AspNetCoreRateLimit;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Business.Mapping.AutoMapper;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);   

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors();

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

builder.Services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule()
            });

//Hangfire Impletation
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFireConnection"));
});
builder.Services.AddHangfireServer();

//Autofac .Net 6+ Implementation
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

//Redis Cache .Net 6+ Impletation 
builder.Services.AddSingleton<RedisCacheManager>();

//AutoMapper .Net 6+ Implementation
builder.Services.AddAutoMapper(typeof(CarProfile));
builder.Services.AddAutoMapper(typeof(CustomerProfile));
builder.Services.AddAutoMapper(typeof(RentalProfile));
builder.Services.AddAutoMapper(typeof(UserProfile));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(builder => builder.WithOrigins("http://localhost:4200", "https://localhost:44313").AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();
//we can see dashboard when we search this routerlink: www.localhost:44313/api/hangfire
app.UseHangfireDashboard("/hangfire", new DashboardOptions()
{
    DashboardTitle = "Rent A Car Hangfire Dashboard"
});

app.MapControllers();

app.Run();
