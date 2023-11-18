using CyprusAirportTransfer.App.Extensions;
using CyprusAirportTransfer.App.Interfaces;
using CyprusAirportTransfer.Infrastructure.Extensions;
using CyprusAirportTransfer.Infrastructure.Services;
using CyprusAirportTransfer.Persistence.Extensions;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.services.
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole(); // You can add other logging providers here if needed
});
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:7266")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
var serviceProvider = builder.Services.BuildServiceProvider();
var botService = serviceProvider.GetService<IChatbotService>() as TelegramBotService;
botService?.StartReceivingUpdates();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
