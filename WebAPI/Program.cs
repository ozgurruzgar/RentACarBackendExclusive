using AspNetCoreRateLimit;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Business.Mapping.AutoMapper;
using Core.CrossCuttingConcerns.Caching.Redis;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);   

// Add services to the container.
builder.Services.AddControllers();

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
builder.Services.AddSingleton<RedisCacheManager>(sp =>
{
    return new RedisCacheManager(builder.Configuration["CacheOptions:Url"]);
});

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

app.UseHttpsRedirection();
//we can see dashboard when we search this routerlink: www.localhost:44413/api/hangfire
app.UseHangfireDashboard("/hangfire");

app.UseAuthorization();

app.MapControllers();

app.Run();
