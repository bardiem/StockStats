using Alpaca.Markets;
using Microsoft.EntityFrameworkCore;
using StockStats.BL;
using StockStats.DAL;
using StockStats.Data;
using StockStats.SL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddDbContext<StockStatsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultCS"));
});

builder.Services.AddScoped<ISymbolPerformanceRepo, SymbolPerformanceRepo>();
builder.Services.AddScoped<ISymbolRepo, SymbolRepo>();

builder.Services.AddScoped<ISymbolSL>(_ => {
        var secretKey = new SecretKey(builder.Configuration.GetValue<string>("AlpacaApi_API_KEY"), builder.Configuration.GetValue<string>("AlpacaApi_API_SECRET"));
        var client = Alpaca.Markets.Environments.Live.GetAlpacaDataClient(secretKey);
        return new SymbolSL(client);
    }
);

builder.Services.AddScoped<ISymbolBL, SymbolBL>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();
