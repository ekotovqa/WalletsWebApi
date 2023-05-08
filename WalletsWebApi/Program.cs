using Core.Data;
using Core.Services;
using Core.Services.Interface;
using Microsoft.EntityFrameworkCore;
using WalletsWebApi;
using WalletsWebApi.Services;
using WalletsWebApi.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOptions<AppSettings>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("WalletsDb")));
builder.Services.AddHostedService<BackgroundWorkerService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddSingleton<IWeb3Service, Web3Service>();

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
