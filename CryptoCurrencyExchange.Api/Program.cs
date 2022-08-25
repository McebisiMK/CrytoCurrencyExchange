using CryptoCurrencyExchange.Core.Abstractions;
using CryptoCurrencyExchange.Core.Services;
using CryptoCurrencyExchange.Data.Abstractions;
using CryptoCurrencyExchange.Data.External;
using CryptoCurrencyExchange.Data.Models;
using CryptoCurrencyExchange.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var connectionString = builder.Configuration.GetConnectionString("CurrencyExchangeDB");
builder.Services.AddDbContext<CurrencyExchangeRatesDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddHttpClient("coinbase", client =>
{
    var baseUrl = builder.Configuration.GetValue<string>("coinbaseApiConfig:baseUrl");
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddTransient<IExchangeRateClient, ExchangeRateClient>();
builder.Services.AddTransient<IExchangeRatesService, ExchangeRatesService>();
builder.Services.AddTransient<IExchangeRateRepository, ExchangeRateRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
