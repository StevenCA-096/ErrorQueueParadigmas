using DataAccess.Context;
using ErrorQueue.DatabaseSettings;
using ErrorQueue.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Reflection.Metadata;
using Quartz;
using Quartz.Impl;
using ErrorQueue;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

//configurar eel job
// 1. Create a scheduler Factory
ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

// 2. Get and start a scheduler
IScheduler scheduler = await schedulerFactory.GetScheduler();
await scheduler.Start();

// 3. Create a job
IJobDetail job = JobBuilder.Create<NumberGeneratorJob>()
        .WithIdentity("number generator job", "number generator group")
        .Build();

// 4. Create a trigger
Quartz.ITrigger trigger = Quartz.TriggerBuilder.Create()
    .WithIdentity("number generator trigger", "number generator group")
    .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
    .Build();

// 5. Schedule the job using the job and trigger 
await scheduler.ScheduleJob(job, trigger);

app.Run();


