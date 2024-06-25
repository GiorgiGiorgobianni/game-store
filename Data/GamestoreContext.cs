using GameStore.api.entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.api.Data;

public class GamestoreContext(DbContextOptions<GamestoreContext> options)
    : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();
}
