using GameStore.api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Date Source=GameStore.db";
builder.Services.AddSqlite<GamestoreContext>(connectionString);


var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
