using DataAccess.Context;
using ErrorQueue.Controllers;
using ErrorQueue.DatabaseSettings;
using ErrorQueue.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<FailedStatusController>();
builder.Services.AddDbContext<ErrorQueueContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ErrorQueueBD") ??
throw new InvalidOperationException("Connection string 'ErrorQueueBD' not found.")));

builder.Services.Configure<ShoppingCartDatabaseSettings>(
    builder.Configuration.GetSection(nameof(ShoppingCartDatabaseSettings))
    );      

builder.Services.AddSingleton<IShoppingCartDatabaseSettings>(
    SP => SP.GetRequiredService<IOptions<ShoppingCartDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>
    ("ShoppingCartDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IShoppingCartService,ShoppingCartService>();
builder.Services.AddScoped<IFailedStatusService, FailedStatusService>();
builder.Services.AddScoped<IQueueService, QueueService>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});


builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
