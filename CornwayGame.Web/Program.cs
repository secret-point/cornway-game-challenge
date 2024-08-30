using CornwayGame.BL;
using CornwayGame.BL.GameRules;
using CornwayGame.BL.Interfaces;
using CornwayGame.BL.Model;
using CornwayGame.Data;
using CornwayGame.Data.Interfaces;
using CornwayGame.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGameRules, GameRules>();
builder.Services.Configure<GameSettings>(
    builder.Configuration.GetSection("GameSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
