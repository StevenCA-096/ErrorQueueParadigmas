using DataAccess.Context;
using ErrorQueue.DatabaseSettings;
using ErrorQueue.Quartz;
using ErrorQueue.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Quartz;
using Quartz.Impl;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ErrorQueueContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ErrorQueueBD") ??
throw new InvalidOperationException("Connection string 'ErrorQueueBD' not found.")));


//Busca la seccion en el appsettings con el nombre ShoppingCartDatabaseSettings
builder.Services.Configure<ShoppingCartDatabaseSettings>(
    builder.Configuration.GetSection(nameof(ShoppingCartDatabaseSettings))
    );      
//Agrega a la interfaz los valores del appsettings
builder.Services.AddSingleton<IShoppingCartDatabaseSettings>(
    SP => SP.GetRequiredService<IOptions<ShoppingCartDatabaseSettings>>().Value);
//Crea el cliente de mongo
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>
    ("ShoppingCartDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IShoppingCartService,ShoppingCartService>();

builder.Services.AddInfrastructure();

//ISchedulerFactory schedulerFactory = new StdSchedulerFactory();


//IScheduler scheduler = await schedulerFactory.GetScheduler();

//await scheduler.Start();

//IJobDetail jobDetail = JobBuilder.Create<ShppingCartJob>().WithIdentity("Shopping car job", "Shopping cart job group").Build();


//Quartz.ITrigger trigger = Quartz.TriggerBuilder.Create().WithIdentity("Shopping cart trigger", "Shopping cart job group").Build();

//await scheduler.ScheduleJob(jobDetail, trigger);

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
