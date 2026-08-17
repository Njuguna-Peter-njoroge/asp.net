using GameStore.Api.Dtos;

namespace GameStore.Api;


public static class GamesEndpoints


{
    const string getGameEndpointName = "GetName";

   private static  readonly  List<GameDto> games = [
    new
     (1,
     "Street fighter II",
      "Fighting", 19.99M,
       new DateOnly (1992,7,7)),
         new
     (2,
     "Street fighter III",
      "Fighting", 19.29M,
       new DateOnly (1942,7,7)),

          new
     (3,
     "Street fighter IV",
      "Fighting", 19.59M,
       new DateOnly (1292,7,7)),

          new
     (4,
     "Street fighter II",
      "Fighting", 19.09M,
       new DateOnly (1991,7,7)),

             new
     (5,
     "Street fighter II",
      "Fighting", 19.00M,
       new DateOnly (1912,7,7)),
       

       
];

public static void MapGamesEndpoints(this WebApplication app)
{


    var group = app.MapGroup("/games");
    // GET / games 
app.MapGet("/", () => games);


// GET / games/id

group.MapGet("/{id}", (int Id ) =>
{
 var game =  games.Find(game =>  game.Id == Id);

return game is null ? Results.NotFound(): Results.Ok(game);

})
.WithName(getGameEndpointName);

//POST /games

group.MapPost("/", (createGamesDto newGame) =>

{
 GameDto game = new(
    games.Count  + 1,
    newGame.Name,
    newGame.Genre,
    newGame.Price,
    newGame.ReleaseDate
 );

 games.Add(game);

 return Results.CreatedAtRoute(getGameEndpointName, new{id = game.Id}, game);

});


// PUT games/1

group.MapPut("/{id}", (int id , updateGameDto updatedGame) =>
{
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1) {

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

//DELETE gsmes/1

group.MapDelete("/{id}", (int id ) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();

});
}
}