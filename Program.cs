using AptOnline.Api;
using AptOnline.Api.Infrastructures.Services;
using AptOnline.Api.Workers;
using AptOnline.Api.Helpers;
using MassTransit;
using System.Reflection;
using AptOnline.Api.Infrastructures.Repos;
using AptOnline.Helpers;
using AptOnline.Infrastructure.LocalContext.PenjualanAgg;
using AptOnline.Infrastructure.LocalContext.ResepRsAgg;
using AptOnline.Infrastructure.BillingContext.DokterAgg;
using AptOnline.Infrastructure.BillingContext.LayananAgg;
using AptOnline.Infrastructure.BpjsContext.DphoAgg;

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
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.SECTION_NAME));
//Register Services
builder.Services.AddScoped<IGetDuFarmasiService, GetDuFarmasiService>();
builder.Services.AddScoped<IGetResepFarmasiService, GetResepFarmasiService>();
builder.Services.AddScoped<IGetSepBillingService, GetSepBillingService>();
builder.Services.AddScoped<IListRefDphoBpjsService, ListRefDphoBpjsService>();
builder.Services.AddScoped<IListRefObatBpjsService, ListRefObatBpjsService>();
builder.Services.AddScoped<IListRefPoliBpjsService, ListRefPoliBpjsService>();
builder.Services.AddScoped<IListRefFaskesBpjsService, ListRefFaskesBpjsService>();
builder.Services.AddScoped<IGetSettingPpkBpjsService, GetSettingPpkBpjsService>();
builder.Services.AddScoped<IInsertResepBpjsService, InsertResepBpjsService>();
builder.Services.AddScoped<IDeleteResepBpjsService, DeleteResepBpjsService>();
builder.Services.AddScoped<IInsertObatBpjsService, InsertObatBpjsService>();
builder.Services.AddScoped<IGetMapDpho, GetMapDpho>();
builder.Services.AddScoped<IGetLayananBillingService, GetLayananBillingService>();
builder.Services.AddScoped<IGetDokterBillingService, GetDokterBillingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IResepRequestBuilder, ResepRequestBuilder>();
builder.Services.AddScoped<IItemNonRacikBuilder, ItemNonRacikBuilder>();
builder.Services.AddScoped<IItemRacikBuilder, ItemRacikBuilder>();
builder.Services.AddScoped<IResepWriter, ResepWriter>();

builder.Services.AddScoped<IResepDal, ResepDal>();
builder.Services.AddScoped<IResepItemDal, ResepItemDal>();

builder.Services.AddTransient<ILogDal, LogDal>();

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
LogHelper.Initialize(app.Services);

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
