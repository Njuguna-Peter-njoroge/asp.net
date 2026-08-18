using GameStore.Api;
using GameStore.Api.Dtos;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var connString = "Host=localhost;Port=5432;Database=GamesDb;Username=postgres;Password=postgres";
builder.Services.AddNpgsql<GameStoreContext>(connString);


var app = builder.Build();

app.MapGamesEndpoints();

app.Run();