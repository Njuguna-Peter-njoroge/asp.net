using GameStore.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api;


public static class GamesEndpoints


{
    const string getGameEndpointName = "GetName";

   

public static void MapGamesEndpoints(this WebApplication app)
{


    var group = app.MapGroup("/games");
    // GET / games 
group.MapGet("/", async(GameStoreContext dbContext) 
 => await dbContext.Games
 .Include(game => game.Genre)
 .Select(game => new GameSummaryDto
 ( 
     game.Id,
     game.Name,
     game.Genre!.Name,
     game.Price,
     game.ReleaseDate
 ))
 .AsNoTracking()
 .ToListAsync());


// GET / games/id

group.MapGet("/{id}", async(int Id, GameStoreContext dbContext) =>
{
 var game =await dbContext.Games.FindAsync(Id);




return game is null ? Results.NotFound(): Results.Ok(
    new GamesDetailsDto(
        game.Id,
        game.Name,
        game.GenreId,
        game.Price,
        game.ReleaseDate
    )
);

})
.WithName(getGameEndpointName);

//POST /games

group.MapPost("/", async(createGamesDto newGame, GameStoreContext dbContext) =>

{
Game game = new ()
{
    Name = newGame.Name,
    GenreId = newGame.GenreId,
    Price = newGame.Price,
    ReleaseDate = newGame.ReleaseDate
    

};

dbContext.Games.Add(game);
await dbContext.SaveChangesAsync().ContinueWith(task => {
        //contunue with logic 

});

GamesDetailsDto gameDto = new(
    game.Id,
    game.Name,
    game.GenreId,
    game.Price,
    game.ReleaseDate

);
 return Results.CreatedAtRoute(getGameEndpointName, new{id = gameDto.Id}, gameDto);

});


// PUT games/1

group.MapPut("/{id}", async (int id , updateGameDto updatedGame, 
  GameStoreContext dbContext
  
  ) =>
{
var existingGame = await dbContext.Games.FindAsync(id);

    if (existingGame is null) {

        return Results.NotFound();
    }

existingGame.Name = updatedGame.Name;
existingGame.GenreId = updatedGame.GenreId;
existingGame.Price = updatedGame.Price;
existingGame.ReleaseDate = updatedGame.ReleaseDate; 

await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

//DELETE gsmes/1

group.MapDelete("/{id}", async(int id, GameStoreContext dbContext ) =>
{
await dbContext.Games
.Where(game => game.Id == id)
.ExecuteDeleteAsync();
    return Results.NoContent();

});
}
}