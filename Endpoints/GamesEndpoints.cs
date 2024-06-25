using GameStore.api.Dtos;

namespace GameStore.api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new(
            1,
            "Cyberpunk 2077",
            "RPG",
            39.99M,
            new DateOnly(2020, 12, 10)
        ),
        new(
            2,
            "World of Warcraft: Wrath of the  Lich King",
            "MMORPG",
            49.99M,
            new DateOnly(2008, 11 ,13)
        ),
    ];

    public static WebApplication MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
            .WithParameterValidation();

        //GET method
        //get list of games
        group.MapGet("/", () => games);

        //get a game using ID
        group.MapGet("/{id}", (int id) => {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        //POST method
        // new game entry
        group.MapPost("/", (CreateGameDto newGame) => {
            if(string.IsNullOrEmpty(newGame.Name)){
                return Results.BadRequest("Name is required");
            }
            GameDto game = new(
                games.Count +1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id}, game); 
        })
        .WithParameterValidation();


        //PUT method
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => {
            var index = games.FindIndex(game => game.Id == id);

            if(index < 0){
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        //Delete method
        group.MapDelete("/{id}", (int id) =>{
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return app;
    }
}
