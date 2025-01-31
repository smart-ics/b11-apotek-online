using AptOnline.Api;
using AptOnline.Api.Services;
using AptOnline.Api.Usecases;
using AptOnline.Helpers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
// Add services to the container.
builder.Configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{Environment.MachineName}.json", true, true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => 
cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.Configure<FarmasiOptions>(builder.Configuration.GetSection(FarmasiOptions.SECTION_NAME));
builder.Services.Configure<BillingOptions>(builder.Configuration.GetSection(BillingOptions.SECTION_NAME));
builder.Services.Configure<BpjsOptions>(builder.Configuration.GetSection(BpjsOptions.SECTION_NAME));
//Register Services
builder.Services.AddScoped<IGetDuFarmasiService, GetDuFarmasiService>();
builder.Services.AddScoped<IGetResepFarmasiService, GetResepFarmasiService>();
builder.Services.AddScoped<IGetSepBillingService, GetSepBillingService>();
builder.Services.AddScoped<IListRefDphoBpjsService, ListRefDphoBpjsService>();
builder.Services.AddScoped<IInsertResepBpjsService, InsertResepBpjsService>();
builder.Services.AddScoped<IInsertObatBpjsService, InsertObatBpjsService>();
builder.Services.AddScoped<IGetMapDpho, GetMapDpho>();
builder.Services.AddScoped<IGetLayananBillingService, GetLayananBillingService>();
builder.Services.AddScoped<IGetDokterBillingService, GetDokterBillingService>();
builder.Services.AddScoped<ITokenService, TokenService>();

//Masstransit
var configuration = builder.Configuration;
var rabbitMqOption = configuration.GetSection("RabbitMqOption");
var enableRabbitMq = rabbitMqOption["Enabled"] == "1";
if (enableRabbitMq)
    builder.Services.AddMassTransit(x =>
    {
        x.SetKebabCaseEndpointNameFormatter();
        x.AddConsumers(typeof(ApplicationAssemblyAnchor).Assembly);
        var server = rabbitMqOption["Server"];
        var userName = rabbitMqOption["UserName"];
        var password = rabbitMqOption["Password"];
   
        x.UsingRabbitMq((context, config) =>
        {
            config.Host(server, "/", h =>
            {
                h.Username(userName ?? string.Empty);
                h.Password(password ?? string.Empty);
            });
            config.ConfigureEndpoints(context);
        });
    });

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
