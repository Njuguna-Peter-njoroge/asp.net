using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions

{ 

public static void MigrateDb(this WebApplication app)
{
using var scope = app.Services.CreateScope();
var dbContect = scope.ServiceProvider
.GetRequiredService<GameStoreContext>();
dbContect.Database.Migrate();
}
public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        
var connString = "Host=localhost;Port=5432;Database=GamesDb;Username=postgres;Password=post123";
builder.Services.AddNpgsql<GameStoreContext>(
    connString,
    optionsAction: options => options.UseSeeding((context, _) =>
    {
        if(!context.Set<Genre>().Any())
        {
            context.Set<Genre>().AddRange(
                new Genre {Name = "Fighting"},
                new Genre {Name = "Shooter"},
                new Genre {Name = "Racing"},
                new Genre {Name = "Sports"},
                new Genre {Name = "Adventure"},
                new Genre {Name = "Action"}
            );

            context.SaveChanges();
        }
    })
    );

    }
}